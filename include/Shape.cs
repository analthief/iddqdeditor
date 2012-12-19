using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;

using Usermods.Fpoint;
using Usermods.Shape;

//=======================================
//Shape - базовый класс для фигур, предо-
//ставляющий следущие методы:
//
//void Draw(..) - отрисовка
//void SaveBinary(..) -сохранение в поток
//Shape LoadBinary(..) -загрузка из пото-
//ка
//=======================================

namespace Usermods.Shape
{
    //Класс, представлящий собой контейнер делегатов для передачи нескольких функций между объектами
    //TODO: сделать универсальный контейнер со списком или ХэшТэйблом
    public class DelegateContainer
    {
        public delegate fpoint dScreenToReal(Point pt);
        public delegate Point dRealToScreen(fpoint pt);
        public delegate Graphics dGetGraphics();

        public dScreenToReal fScreenToReal;
        public dRealToScreen fRealToSreeen;
        public dGetGraphics fGetGraphics;
    }

    //Класс фигуры
    public abstract class Shape
    {
        protected DelegateContainer dlc;
        public abstract string description {get;}
        
        protected Shape(DelegateContainer _dlc)
        {
            this.dlc = _dlc;
        }

        //отрисовка фигуры заданным цветом (Graphics нам возвратит DelegateContainer)
        public abstract void Draw(Color clr);
        //отрисовка фигуры черным цветом
        public virtual	void Draw()
        {
            Draw(Color.Black);
        }
		
        //Сохранить в бинарном формате
        public abstract void SaveBinary(BinaryWriter bw);

        //Загрузить в бинарном формате
        public static Shape LoadBinary(BinaryReader br, DelegateContainer _dlc)
        {
            byte Signature = br.ReadByte();
            switch (Signature)
            {
                case 0:
                    return new sCross(_dlc, br.ReadDouble(), br.ReadDouble());
                case 1:
                    return new sLine(_dlc, br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                case 2:
                    return new sCircle(_dlc, br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                case 3:
                    return new sRect(_dlc, br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                default: return null;
            }
        }

        //Получает расстояние от данной точки до фигуры
        public abstract double GetR(fpoint f);
    }

    //Крестик
    public class sCross : Shape
    {
        public static byte Sign = 0;
        //центр
        private fpoint Center;
        private Pen _pen;
        
        public override string description
        {
            get
            {
                return "[" + Center.x.ToString() + ";" + Center.y.ToString() + "]";
            }
        }

        public sCross(DelegateContainer _dlc, int x, int y)
            : base(_dlc)
        {
            Center = base.dlc.fScreenToReal(new Point(x, y));
        }

        public sCross(DelegateContainer _dlc, double x, double y)
            : base(_dlc)
        {
            Center = new fpoint(x, y);
        }

        public override void Draw(Color clr)
        {
            _pen = new Pen(clr);
            Point spt = base.dlc.fRealToSreeen(Center);

            base.dlc.fGetGraphics().DrawLine(_pen, spt.X - 3, spt.Y - 3, spt.X + 3, spt.Y + 3);
            base.dlc.fGetGraphics().DrawLine(_pen, spt.X + 3, spt.Y - 3, spt.X - 3, spt.Y + 3);
        }

        public override void SaveBinary(BinaryWriter bw)
        {
            bw.Write(Sign);
            bw.Write(Center.x);
            bw.Write(Center.y);
        }

        public override double GetR(fpoint f)
        {
            double val = fpoint.GetRasst(this.Center, f);
            return val < 50 ? val: -1;
        }
    }

    //Линия
    public class sLine : Shape
    {
        public static byte Sign = 1;
        //точка начала
        private fpoint fpBeg;
        //точка конца
        private fpoint fpEnd;
        private Pen _pen;

        public override string description
        {
            get
            {
                return "[" + fpBeg.x.ToString() + ";" + fpBeg.y.ToString() + "]," +
                "[" + fpEnd.x.ToString() + ";" + fpEnd.y.ToString() + "]";
            }
        }

        public sLine(DelegateContainer _dlc, Point p1, Point p2)
            : base(_dlc)
        {
            fpBeg = base.dlc.fScreenToReal(p1);
            fpEnd = base.dlc.fScreenToReal(p2);
        }

        public sLine(DelegateContainer _dlc, double x1, double y1, double x2, double y2)
            : base(_dlc)
        {
            fpBeg = new fpoint(x1, y1);
            fpEnd = new fpoint(x2, y2);
        }

        public override void Draw(Color clr)
        {
            Point spt_beg = base.dlc.fRealToSreeen(fpBeg);
            Point spt_end = base.dlc.fRealToSreeen(fpEnd);

            _pen = new Pen(clr);
            base.dlc.fGetGraphics().DrawLine(_pen, spt_beg, spt_end);
        }

        public override void SaveBinary(BinaryWriter bw)
        {
            bw.Write(Sign);
            bw.Write(fpBeg.x);
            bw.Write(fpBeg.y);
            bw.Write(fpEnd.x);
            bw.Write(fpEnd.y);
        }

        public override double GetR(fpoint f)
        {
            fpoint bp;
            fpoint cp;

            if (fpoint.GetRasst(f, fpEnd) > fpoint.GetRasst(f, fpBeg))
            {
                bp = fpEnd - fpBeg;
                cp = f - fpBeg;
            }
            else
            {
                bp = fpBeg - fpEnd;
                cp = f - fpEnd;
            }

            double a = fpoint.GetRasstNormalized(cp);
            double b = fpoint.GetRasstNormalized(bp);
            double cosa = (bp.x * cp.x + bp.y * cp.y) / (a * b);
            double res = cosa >= 0 ? Math.Sqrt(1 - cosa * cosa) * a : -1;

            return res < 50 ? res : -1;
        }
    }

    //Окружность
    public class sCircle : Shape
    {
        public static byte Sign = 2;
        //Центр
        private fpoint Center;
        //Радиус
        private double Radius;
        private Pen _pen;

        public override string description { 
            get{
            return "[" + Center.x.ToString() + ";" + Center.y.ToString() + "] r=" + Radius.ToString();
            }
        }

        public sCircle(DelegateContainer _dlc, Point Down, Point Up)
            : base(_dlc)
        {
            this.Center = base.dlc.fScreenToReal(Down);
            this.Radius = fpoint.GetRasst(base.dlc.fScreenToReal(Down), base.dlc.fScreenToReal(Up));
        }

        public sCircle(DelegateContainer _dlc, double x1, double y1, double r)
            : base(_dlc)
        {
            this.Center = new fpoint(x1, y1);
            this.Radius = r;
        }

        //Сводка:
        //    Получает Rectangle для текущего объекта. Вторые два параметра - НЕ ДЛИНА И ШИРИНА.
        //    Это - координаты нижнего правого угла 
        public Rectangle GetRect()
        {
            Point int_uppoint = base.dlc.fRealToSreeen(new fpoint(this.Center.x - Radius, this.Center.y - Radius));
            Point int_dwpoint = base.dlc.fRealToSreeen(new fpoint(this.Center.x + Radius, this.Center.y + Radius));

            return new Rectangle(int_uppoint.X, int_uppoint.Y, int_dwpoint.X, int_dwpoint.Y);
        }

        public override void Draw(Color clr)
        {
            Point int_uppoint = base.dlc.fRealToSreeen(new fpoint(this.Center.x - Radius, this.Center.y - Radius));
            Point int_dwpoint = base.dlc.fRealToSreeen(new fpoint(2*Radius,2*Radius));

            _pen = new Pen(clr);
            base.dlc.fGetGraphics().DrawEllipse(_pen, int_uppoint.X, int_uppoint.Y, int_dwpoint.X, int_dwpoint.Y);
        }

        public override void SaveBinary(BinaryWriter bw)
        {
            bw.Write(Sign);
            bw.Write(Center.x);
            bw.Write(Center.y);
            bw.Write(Radius);
        }

        public override double GetR(fpoint f)
        {
            double val = fpoint.GetRasst(Center, f);
            return val < Radius ? val : -1;
        }
    }

    //Прямоугольник
    public class sRect : Shape
    {
        public static byte Sign = 3;
        //Верхний левый
        private fpoint Up;
        //Нижний правый
        private fpoint Down;
        private Pen _pen;

        public override string description
        {
            get
            {
               return "[" + Up.x.ToString() + ";" + Up.y.ToString() + "]," +
                     "[" + Down.x.ToString() + ";" + Down.y.ToString() + "]";;
            }
        }

        public sRect(DelegateContainer _dlc, Point Down, Point Up)
            : base(_dlc)
        {
            this.Up = base.dlc.fScreenToReal(Down);
            this.Down = base.dlc.fScreenToReal(Up);
        }

        public sRect(DelegateContainer _dlc, double x1, double y1, double x2, double y2)
            : base(_dlc)
        {
            this.Up = new fpoint(x1, y1);
            this.Down = new fpoint(x2, y2);
        }

        private void WHRect(out fpoint a, out fpoint b)
        {
            fpoint uppoint;
            fpoint dwpoint;

            if (Up.y < Down.y)
            {
                uppoint = Up;
                dwpoint = Down;
            }
            else
            {
                uppoint = Down;
                dwpoint = Up;
            }

            if (uppoint.x > dwpoint.x)
            {
                double temp = dwpoint.y;
                dwpoint.y = uppoint.y;
                uppoint.y = temp;

                fpoint tmp = dwpoint;
                dwpoint = uppoint;
                uppoint = tmp;
            }

            a = uppoint;
            b = dwpoint;
        }

        public override void Draw(Color clr)
        {
            fpoint a;
            fpoint b;
            WHRect(out a, out b);

            Point int_uppoint = base.dlc.fRealToSreeen(a);
            Point int_dwpoint = base.dlc.fRealToSreeen(b);

            _pen = new Pen(clr);
            base.dlc.fGetGraphics().DrawRectangle(_pen, int_uppoint.X, int_uppoint.Y, int_dwpoint.X - int_uppoint.X, int_dwpoint.Y - int_uppoint.Y);
        }

        public Rectangle GetRect()
        {
            Point int_uppoint = base.dlc.fRealToSreeen(Up);
            Point int_dwpoint = base.dlc.fRealToSreeen(Down);

            return new Rectangle(int_uppoint.X, int_uppoint.Y, int_dwpoint.X, int_dwpoint.Y);
        }

        public override void SaveBinary(BinaryWriter bw)
        {
            bw.Write(Sign);
            bw.Write(Up.x);
            bw.Write(Up.y);
            bw.Write(Down.x);
            bw.Write(Down.y);
        }

        public override double GetR(fpoint f)
        {
            fpoint a;
            fpoint b;
            WHRect(out a, out b);

            if ((f.x > a.x) && (f.y > a.y) && (f.x < b.x) && (f.y < b.y)) return 10;
            return -1;
        }
    }
}
