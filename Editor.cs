﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

using Usermods.Shape;
using Usermods.Fpoint;
using Usermods.ExtList;

namespace Sharp
{
    public class Editor
    {
        private int[] SelectedIndexi;

        //List with points
        private ExtList<Shape> shapelist;

        //On modification
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

        //filename
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
        private dExceptionHandler onException;

        //обработчик события, когда нужно перерисовать Canvas
        public delegate void dRefreshHandler();
        public dRefreshHandler onRefresh;

        //обработчик события, когда нужно изменить заголовок
        public delegate void dCapRefreshHandler(int index, string a);
        private dCapRefreshHandler onFileNameChanged;

        //обработчик события, когда изменился список фигур
        public delegate void dShapesModificationHandler(int index, ref ExtList<Shape> s);
        private dShapesModificationHandler onShapesChanged;


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

        private DelegateContainer GetDeleateContainer()
        {
            DelegateContainer dlc = new DelegateContainer();
            dlc.fGetGraphics = new DelegateContainer.dGetGraphics(GetGraphics);
            dlc.fRealToSreeen = new DelegateContainer.dRealToScreen(RealToScreen);
            dlc.fScreenToReal = new DelegateContainer.dScreenToReal(ScreenToReal);

            return dlc;
        }

        private Graphics GetGraphics()
        {
            return this.screen;
        }

        private void shapelist_AfterChanged(object sender)
        {
            onShapesChanged(this.internal_index, ref shapelist);
        }

        private void shapelist_BeforeChanged(object sender)
        {
            shapelist_modificated = true;
        }

        //сохраняет фигуру в файл
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

        public void SaveToFile()
        {
            this.SaveToFile(this.cur_fname);
        }

        //load from file
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

        //назначает обработчик исключений для ошибок в классе (система событий)
        public void setExceptionHandler(dExceptionHandler func)
        {
            onException = func;
        }

        //назначает обработчик обновления дополнительного кона
        public void setRefreshHandler(dRefreshHandler func)
        {
            onRefresh = func;
        }

        //назначает обработчик события перерисовки заголовка вкладки
        public void setCaptRefreshHandler(dCapRefreshHandler func)
        {
            onFileNameChanged = func;
        }

        //назначает обработчик события изменения фигур в списке для вывода их
        //на форме
        public void setShapesChangedHandler(dShapesModificationHandler func)
        {
            onShapesChanged = func;
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

        private void FillSelectedBlack(int[] oldindexes)
        {
            if (oldindexes == null) return;

            foreach (int index in oldindexes)
            {
                if ((index >= 0) && (index < shapelist.Count))
                    ReDrawShape(shapelist[index], Color.Black);
            }
        }

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

        public void DeleteFigures(int[] indexes)
        {
            int delta = 0;
            foreach (int index in indexes)
            {         
                shapelist.RemoveAt(index-delta);
                delta++;
            }
        }

        private void ReDrawShape(Shape sp, Color clr)
        {
            sp.Draw(clr);
        }

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

        //редефайнит координтаты. нужно, если меняем масштаб избражения (основного),
        //при этом также переприсваивается Битмэп для отрисовки
        //и изменяются коэффициенты пересчета координат
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

        //синхронизирует канву и битмэп
        public void SynchronizeImage()
        {
            cnv.Image = bmp;
            onRefresh();
        }

        //очищает все
        public void ClearList()
        {
            shapelist.Clear();
            return;
        }

        //ОБРАБОТКА ФИГУР
        public void AddCross(int x, int y)
        {
            sCross cross = new sCross(GetDeleateContainer(), x, y);
            shapelist.Add(cross);

            cross.Draw();
            SynchronizeImage();
        }

        public void AddLine(Point p1, Point p2)
        {
            sLine line = new sLine(GetDeleateContainer(), p1, p2);
            shapelist.Add(line);

            line.Draw();
            SynchronizeImage();
        }

        public void AddEllipse(Point up, Point down)
        {
            sCircle circle = new sCircle(GetDeleateContainer(), up, down);
            shapelist.Add(circle);

            circle.Draw();
            SynchronizeImage();
        }

        private Point oldb;
        public void TempLine(Point a, Point b)
        {
            IntPtr hdc = Gdi32.GetDC(cnv.Handle);
            Gdi32.SetROP2(hdc, Gdi32.R2_NOT);

            if ((oldb.X != int.MinValue) & (oldb.Y != int.MinValue))
            {
                Gdi32.MoveToEx(hdc, a.X, a.Y, IntPtr.Zero);
                Gdi32.LineTo(hdc, oldb.X, oldb.Y);
            }

            Gdi32.MoveToEx(hdc, a.X, a.Y, IntPtr.Zero);
            Gdi32.LineTo(hdc, b.X, b.Y);
            
            oldb = b;
            Gdi32.ReleaseDC(cnv.Handle, hdc);
        }

        public void TempEllipse(Point a, Point b)
        {
            IntPtr hdc = Gdi32.GetDC(cnv.Handle);
            sCircle Circle;
            Rectangle rect;

            Gdi32.SetROP2(hdc, Gdi32.R2_NOT);
            Gdi32.SelectObject(hdc, Gdi32.GetStockObject(Gdi32.HOLLOW_BRUSH));
            if ((oldb.X != int.MinValue) & (oldb.Y != int.MinValue))
            {
                Circle = new sCircle(GetDeleateContainer(), a, oldb);
                rect = Circle.GetRect();

                Gdi32.Ellipse(hdc, rect.X, rect.Y, rect.Width, rect.Height);
            }

            Circle = new sCircle(GetDeleateContainer(), a, b);
            rect = Circle.GetRect();
            Gdi32.Ellipse(hdc, rect.X, rect.Y, rect.Width, rect.Height);
            
            oldb = b;
            Gdi32.ReleaseDC(cnv.Handle, hdc);
        }

        public void PrepareToTempDraw()
        {
            oldb.X = int.MinValue;
            oldb.Y = int.MinValue;
        }

    }
}
