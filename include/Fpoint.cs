using System;

//=======================================
//Реализует класс точки с плавающими
//(double) координатами. Определяет опе-
//раторы +,-,==,!=,>,<
//=======================================

namespace Usermods.Fpoint
{
    public class fpoint
    {
        public double x;
        public double y;

        //init 
        public fpoint(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        //create void point
        public fpoint()
        {
            x = 0;
            y = 0;
        }

        public bool IsZero()
        {
            return ((this.x == 0) && (this.y == 0));
        }

        //-
        public static fpoint operator -(fpoint a, fpoint b)
        {
            return new fpoint(a.x - b.x, a.y - b.y);
        }

        //+
        public static fpoint operator +(fpoint a, fpoint b)
        {
            return new fpoint(a.x + b.x, a.y + b.y);
        }

        //>
        public static bool operator >(fpoint a, fpoint b)
        {
            return a.x > b.x || a.y > b.y;
        }

        //<
        public static bool operator <(fpoint a, fpoint b)
        {
            return a.x < b.x || a.y < b.y;
        }

        //==
        public static bool operator ==(fpoint a, fpoint b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }

        //!=
        public static bool operator !=(fpoint a, fpoint b)
        {
            return (a.x != b.x) || (a.y != b.y);
        }

        //Расстояние между двумя точками
        public static double GetRasst(fpoint a, fpoint b)
        {
            return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }

        //Длина вектора с началом в точке (0,0)
        public static double GetRasstNormalized(fpoint a)
        {
            return Math.Sqrt(a.x * a.x + a.y * a.y);
        }
    }
}
