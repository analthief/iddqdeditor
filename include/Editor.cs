using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

using Usermods.Shape;
using Usermods.Fpoint;
using Usermods.ExtList;

namespace Sharp
{
    //Класс представляет средства для редактирования векторных фигур на поле
    public class Editor : IDisposable
    {
        //Выбранные индексы
        private int[] SelectedIndexi;

        //List with points
        private ExtList<Shape> shapelist;

        //Состояние списка фигур: менялись ли они с момента последнего сохранения
        private bool shapelist_modificated_private;
        public bool shapelist_modificated
        {
            get
            {
                return shapelist_modificated_private;
            }
            set
            {
                if (value == shapelist_modificated_private) return;

                shapelist_modificated_private = value;
                if (onFileNameChanged != null)
                {
                    string fname;

                    if (cur_fname == "")
                        fname = "Untitled";
                    else
                        fname = Path.GetFileName(cur_fname_private);

                    if (shapelist_modificated_private)
                        onFileNameChanged(this.internal_index, fname + "*");
                    else
                        onFileNameChanged(this.internal_index, fname);
                }
            }
        }

        //Имя файла, сопоставленное с текущим объектом
        private string cur_fname_private = "";
        public string cur_fname
        {
            get
            {
                return cur_fname_private;
            }
            set
            {
                cur_fname_private = value;
                if (cur_fname_private == "")
                {
                    if (onFileNameChanged != null)
                        onFileNameChanged(this.internal_index, "Untitled");
                }
                else
                {
                    if (onFileNameChanged != null)
                        onFileNameChanged(this.internal_index, Path.GetFileName(cur_fname_private));
                }
            }
        }

        //index in tag
        private int internal_index = 0;

        //H & W of Canvas (graphical component)
        private int screen_w;
        private int screen_h;

        //H & W (real coordinates)
        private double real_w;
        private double real_h;

        //koefs 
        private double sr_dx = 0;
        private double sr_dy = 0;
        private double rs_dx = 0;
        private double rs_dy = 0;

        //winform control
        private PictureBox cnv;
        private Image bmp;
        public Graphics screen;

        //обработчики исключений
        public delegate void dExceptionHandler(string s, string q);
        public event dExceptionHandler onException;

        //обработчик события, когда нужно перерисовать Canvas
        public delegate void dRefreshHandler();
        public event dRefreshHandler onRefresh;

        //обработчик события, когда нужно изменить заголовок
        public delegate void dCapRefreshHandler(int index, string a);
        public event dCapRefreshHandler onFileNameChanged;

        //обработчик события, когда изменился список фигур
        public delegate void dShapesModificationHandler(int index, ref ExtList<Shape> s);
        public event dShapesModificationHandler onShapesChanged;

        // Track whether Dispose has been called.
        private bool disposed = false;


        //констуктор
        public Editor(int x, int y, ref PictureBox canvas, int index)
        {
            real_w = x;
            real_h = y;

            cnv = canvas;
            RedefineXY(x, y);

            shapelist = new ExtList<Shape>();
            shapelist.BeforeChanged += new ChangedEventHandler(shapelist_BeforeChanged);
            shapelist.AfterChanged += new ChangedEventHandler(shapelist_AfterChanged);

            shapelist_modificated_private = false;
            cur_fname = "";
            internal_index = index;
        }

        //Формирует контейнер с функциями, которые нужны для отрисовки фигур
        private DelegateContainer GetDeleateContainer()
        {
            DelegateContainer dlc = new DelegateContainer();
            dlc.fGetGraphics = new DelegateContainer.dGetGraphics(GetGraphics);
            dlc.fRealToSreeen = new DelegateContainer.dRealToScreen(RealToScreen);
            dlc.fScreenToReal = new DelegateContainer.dScreenToReal(ScreenToReal);

            return dlc;
        }

        //Вызываемая функция - возвращает объект @Graphics, сопопставленный с текущим объектом.
        //Используется во внешних вызовах.
        private Graphics GetGraphics()
        {
            return this.screen;
        }

        //Событие, приылаемое перед измением списка фигур
        private void shapelist_AfterChanged(object sender)
        {
            onShapesChanged(this.internal_index, ref shapelist);
        }

        //Событие, посылаемое после изменения списка фигур
        private void shapelist_BeforeChanged(object sender)
        {
            shapelist_modificated = true;
        }

        //Сохраняет список фигур в указанный файл
        public void SaveToFile(string fname)
        {
            try
            {
                FileStream fs = File.Create(fname);
                BinaryWriter binwr = new BinaryWriter(fs);

                foreach (Shape shape in shapelist)
                {
                    shape.SaveBinary(binwr);
                }

                binwr.Close();
                fs.Close();
            }
            catch (Exception exc)
            {
                onException("Ошибка при записи файла, проверьте доступность файла", exc.Message);
                shapelist.Clear();
            }
            finally
            {
                shapelist_modificated = false;
            }
        }

        //Сохраняет список фигур в текущий файл
        public void SaveToFile()
        {
            this.SaveToFile(this.cur_fname);
        }

        //Загружает список фигур из указанного файла
        public void LoadFromFile(string fname)
        {
            try
            {
                FileStream fs = File.OpenRead(fname);
                BinaryReader binrr = new BinaryReader(fs);

                //запрещаем события
                shapelist.Silent = true;

                while (fs.Position < fs.Length)
                {
                    Shape s = Shape.LoadBinary(binrr, GetDeleateContainer());
                    if (s != null) shapelist.Add(s);
                }

                //разрешаем события
                shapelist.Silent = false;

                binrr.Close();
                fs.Close();
            }
            catch (Exception exc)
            {
                onException("Ошибка при чтении файла, проверьте формат", exc.Message);
                shapelist.Clear();
            }
            finally
            {
                shapelist_modificated = false;
            }
        }

        //переводит экранные координаты в истинные
        //точка Point должна принадлежать канве для рисования фигуры, а не форме (!!!)
        public fpoint ScreenToReal(Point pt)
        {
            return new fpoint(pt.X * sr_dx, pt.Y * sr_dy);
        }

        //переводит истинны координаты в экранные
        public Point RealToScreen(fpoint pt)
        {
            try
            {
                return new Point(Convert.ToInt32(pt.x * rs_dx), Convert.ToInt32(pt.y * rs_dy));
            }
            catch (Exception e)
            {
                onException("Ошибка при преобразовании координаты", e.Message);
                return new Point();
            }
        }

        //рефреш
        public void Refresh()
        {
            screen.Clear(Color.White);

            shapelist.ForEach(delegate(Shape shape)
            {
                shape.Draw();
            });

            onShapesChanged(this.internal_index, ref shapelist);

            SynchronizeImage();
        }

        //Выводит выбранные фигуры цветом по умолчанию (визуально отменяет их выбор)
        private void FillSelectedBlack(int[] oldindexes)
        {
            if (oldindexes == null) return;

            foreach (int index in oldindexes)
            {
                if ((index >= 0) && (index < shapelist.Count))
                    ReDrawShape(shapelist[index], Color.Black);
            }
        }

        //Визуально выделяет список выбранных фигур
        public void SelectFigures(int[] indexes)
        {
            //deselect previous
            FillSelectedBlack(SelectedIndexi);

            //nothing selected
            if (indexes.Length == 0)
            {
                SelectedIndexi = new int[0];
                SynchronizeImage();
            }

            //select current
            foreach(int index in indexes)
            {
                if((index >= 0)&&(index < shapelist.Count))
                    ReDrawShape(shapelist[index], Color.Red);
            }

            SelectedIndexi = indexes;
            SynchronizeImage();
        }

        //Удаляет заданный список фигур
        public void DeleteFigures(int[] indexes)
        {
            int delta = 0;
            foreach (int index in indexes)
            {         
                shapelist.RemoveAt(index-delta);
                delta++;
            }
        }

        //Перерисовывает фигуру заданым цветом
        private void ReDrawShape(Shape sp, Color clr)
        {
            sp.Draw(clr);
        }

        //Получает ближайшую фигуру к указанной экранной точке
        public int GetNearShape(Point pt)
        {
            fpoint fpt = ScreenToReal(pt);
            double minval = double.MaxValue;
            double val = 0;
            int minsp = -1;

            for (int i = 0; i < shapelist.Count; i++)
            {
                val = shapelist[i].GetR(fpt);
                if ((val < minval) && (val != -1))
                {
                    minval = val;
                    minsp = i;
                }
            }

            return minsp;
        }

        //Редефайнит координтаты. Нужно, если меняем масштаб избражения (основного),
        //при этом также переприсваивается Битмэп для отрисовки и изменяются коэффициенты пересчета координат.
        public void RedefineXY(int newx, int newy)
        {
            screen_h = newy;
            screen_w = newx;

            sr_dx = Math.Round(real_w / screen_w, 3);
            sr_dy = Math.Round(real_h / screen_h, 3);
            rs_dx = screen_w / real_w;
            rs_dy = screen_h / real_h;

            bmp = new Bitmap(newx, newy);
            screen = Graphics.FromImage(bmp);
        }

        //Синхронизирует канву и битмэп
        public void SynchronizeImage()
        {
            cnv.Image = bmp;
            onRefresh();
        }

        //Очищает список фигур
        public void ClearList()
        {
            shapelist.Clear();
            return;
        }

        //ОБРАБОТКА ФИГУР
        //Добавить крестик
        public void AddCross(int x, int y)
        {
            sCross cross = new sCross(GetDeleateContainer(), x, y);
            shapelist.Add(cross);

            cross.Draw();
            SynchronizeImage();
        }

        //Добавить линию
        public void AddLine(Point p1, Point p2)
        {
            sLine line = new sLine(GetDeleateContainer(), p1, p2);
            shapelist.Add(line);

            line.Draw();
            SynchronizeImage();
        }

        //Добавить эллипс
        public void AddEllipse(Point up, Point down)
        {
            sCircle circle = new sCircle(GetDeleateContainer(), up, down);
            shapelist.Add(circle);

            circle.Draw();
            SynchronizeImage();
        }

        //Добавить прямоугольник
        public void AddRect(Point up, Point down)
        {
            sRect rect = new sRect(GetDeleateContainer(), up, down);
            shapelist.Add(rect);

            rect.Draw();
            SynchronizeImage();
        }

        //предыдущая точка
        private Point oldb;
        //Рисует временную линию средствами gdi32
        public void TempLine(Point a, Point b)
        {
            IntPtr hdc = NativeMethods.GetDC(cnv.Handle);
            NativeMethods.SetROP2(hdc, NativeMethods.R2_NOT);

            if ((oldb.X != int.MinValue) & (oldb.Y != int.MinValue))
            {
                NativeMethods.MoveToEx(hdc, a.X, a.Y, IntPtr.Zero);
                NativeMethods.LineTo(hdc, oldb.X, oldb.Y);
            }

            NativeMethods.MoveToEx(hdc, a.X, a.Y, IntPtr.Zero);
            NativeMethods.LineTo(hdc, b.X, b.Y);
            
            oldb = b;
            NativeMethods.ReleaseDC(cnv.Handle, hdc);
        }

        //Рисует временный прямоугольник средствами gdi32
        public void TempRect(Point a, Point b)
        {
            IntPtr hdc = NativeMethods.GetDC(cnv.Handle);
            sRect Rect;
            Rectangle rect;

            NativeMethods.SetROP2(hdc, NativeMethods.R2_NOT);
            NativeMethods.SelectObject(hdc, NativeMethods.GetStockObject(NativeMethods.HOLLOW_BRUSH));
            if ((oldb.X != int.MinValue) & (oldb.Y != int.MinValue))
            {
                Rect = new sRect(GetDeleateContainer(), a, oldb);
                rect = Rect.GetRect();

                NativeMethods.Rectangle(hdc, rect.X, rect.Y, rect.Width, rect.Height);
            }

            Rect = new sRect(GetDeleateContainer(), a, b);
            rect = Rect.GetRect();

            NativeMethods.Rectangle(hdc, rect.X, rect.Y, rect.Width, rect.Height);
            
            oldb = b;
            NativeMethods.ReleaseDC(cnv.Handle, hdc);
        }

        //Рисует временный эллипс средствами gdi32
        public void TempEllipse(Point a, Point b)
        {
            IntPtr hdc = NativeMethods.GetDC(cnv.Handle);
            sCircle Circle;
            Rectangle rect;

            NativeMethods.SetROP2(hdc, NativeMethods.R2_NOT);
            NativeMethods.SelectObject(hdc, NativeMethods.GetStockObject(NativeMethods.HOLLOW_BRUSH));
            if ((oldb.X != int.MinValue) & (oldb.Y != int.MinValue))
            {
                Circle = new sCircle(GetDeleateContainer(), a, oldb);
                rect = Circle.GetRect();

                NativeMethods.Ellipse(hdc, rect.X, rect.Y, rect.Width, rect.Height);
            }

            Circle = new sCircle(GetDeleateContainer(), a, b);
            rect = Circle.GetRect();
            NativeMethods.Ellipse(hdc, rect.X, rect.Y, rect.Width, rect.Height);

            oldb = b;
            NativeMethods.ReleaseDC(cnv.Handle, hdc);
        }

        //Подготавливает текущий объект к отрисовке временных фигур
        public void PrepareToTempDraw()
        {
            oldb.X = int.MinValue;
            oldb.Y = int.MinValue;
        }

        //for IDisposable
        //мы должны уничтожить объект Bitmap перед удалением объекта
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                bmp.Dispose();
                // Note disposing has been done.
                disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
