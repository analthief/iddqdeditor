using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

//Обертка для библиотеки gdi32.dll
namespace Sharp
{
    public static class NativeMethods
    {
        [DllImport("gdi32.dll", EntryPoint = "SetROP2", CallingConvention = CallingConvention.StdCall)]
        public extern static int SetROP2(IntPtr hdc, int fnDrawMode);

        [DllImport("user32.dll", EntryPoint = "GetDC", CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC", CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", EntryPoint = "MoveToEx", CallingConvention = CallingConvention.StdCall)]
        public extern static bool MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        [DllImport("gdi32.dll", EntryPoint = "LineTo", CallingConvention = CallingConvention.StdCall)]
        public extern static bool LineTo(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll", EntryPoint = "Ellipse", CallingConvention = CallingConvention.StdCall)]
        public extern static bool Ellipse(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll", EntryPoint = "GetStockObject", CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr GetStockObject(int OBJECT);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject", CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr SelectObject(IntPtr hdc, IntPtr OBJECT);

        public const int R2_NOT = 6;  // Inverted drawing mode
        public const int R2_XOR = 7;  // Xor drawing mode
        public const int HOLLOW_BRUSH = 5; //Hollow brush
    }
}
