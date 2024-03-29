﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Desktop
{
    class Snapshot
    {
        public static Bitmap GetWindow(IntPtr hWnd)
        {

            IntPtr hscrdc = GetWindowDC(hWnd); Rect rect = new Rect();
            //Control control = Control.FromHandle(hWnd);
            if (GetWindowRect(hWnd, ref rect) == 0)
            {
                throw new Exception("Get Rect Error");
            }
            IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, (int)((rect.Right - rect.Left) * 1.5), (int)((rect.Bottom - rect.Top) * 1.5));
            IntPtr hmemdc = CreateCompatibleDC(hscrdc);
            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            DeleteDC(hscrdc);//删除用过的对象
            DeleteDC(hmemdc);//删除用过的对象
            return bmp;
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
   string lpszDriver,        // driver name驱动名
   string lpszDevice,        // device name设备名
   string lpszOutput,        // not used; should be NULL
   IntPtr lpInitData  // optional printer data
   );
        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
         IntPtr hdcDest, // handle to destination DC目标设备的句柄
         int nXDest,  // x-coord of destination upper-left corner目标对象的左上角的X坐标
         int nYDest,  // y-coord of destination upper-left corner目标对象的左上角的Y坐标
         int nWidth,  // width of destination rectangle目标对象的矩形宽度
         int nHeight, // height of destination rectangle目标对象的矩形长度
         IntPtr hdcSrc,  // handle to source DC源设备的句柄
         int nXSrc,   // x-coordinate of source upper-left corner源对象的左上角的X坐标
         int nYSrc,   // y-coordinate of source upper-left corner源对象的左上角的Y坐标
         UInt32 dwRop  // raster operation code光栅的操作值
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,        // handle to DC
         int nWidth,     // width of bitmap, in pixels
         int nHeight     // height of bitmap, in pixels
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
         IntPtr hdc,          // handle to DC
         IntPtr hgdiobj   // handle to object
         );

        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
         IntPtr hdc          // handle to DC
         );

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
         IntPtr hwnd,               // Window to copy,Handle to the window that will be copied. 
         IntPtr hdcBlt,             // HDC to print into,Handle to the device context. 
         UInt32 nFlags              // Optional flags,Specifies the drawing options. It can be one of the following values. 
         );

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(
         IntPtr hwnd
         );
        [DllImport("user32.dll")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        public static IntPtr FindWindowByName(string className, string windowName)
        {
            return (IntPtr)FindWindow(className, windowName);
        }

        [DllImport("user32.dll")]
        static extern int GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }
    }
}
