using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Interop;

namespace Tab_Slider
{
    static class Win32
    {
        public const int WM_DESTROY = 2;
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;

        public const int WS_EX_LAYERED = 0x00080000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOACTIVATE = 0x0010;

        public const uint GW_HWNDNEXT = 2;

        public const uint SPI_GETSCREENSAVERRUNNING = 0x0072;

        static public readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        [DllImport("user32.dll")]
        static public extern IntPtr GetWindow(IntPtr hwnd, uint wCmd);
        [DllImport("user32.dll")]
        static public extern bool IsWindowVisible(IntPtr hwnd);
        [DllImport("user32.dll")]
        static public extern bool SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        static public extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        static public extern IntPtr SetParent(IntPtr child, IntPtr parent);
        [DllImport("user32.dll", SetLastError = true)]
        static public extern int SetWindowLong(IntPtr hwnd, int index, int value);
        [DllImport("user32.dll", SetLastError = true)]
        static public extern int GetWindowLong(IntPtr hwnd, int index);
        [DllImport("user32.dll", PreserveSig = false)]
        static public extern void GetWindowRect(IntPtr hwnd, out RECT rect);
        [DllImport("user32.dll")]
        static public extern bool SystemParametersInfo(uint uiAction, uint uiParam, out bool boolValue, uint fwInit);
        static public void SetBottomMost(System.Windows.Window w)
        {
            var hwnd = new WindowInteropHelper(w).Handle;            
            Win32.SetWindowPos(hwnd, Win32.HWND_BOTTOM, 0, 0, 0, 0,
               Win32.SWP_NOSIZE | Win32.SWP_NOMOVE | Win32.SWP_NOACTIVATE);
        }
        static public void SetNoActivate(System.Windows.Window w)
        {
            var hwnd = new WindowInteropHelper(w).Handle;
            

            var newValue = Win32.GetWindowLong(hwnd,
                Win32.GWL_EXSTYLE) | Win32.WS_EX_NOACTIVATE;

            Win32.SetWindowLong(hwnd, Win32.GWL_EXSTYLE, newValue);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width
            {
                get { return Right - Left; }
            }
            public int Height
            {
                get { return Bottom - Top; }
            }
            public override string ToString()
            {
                return string.Format("RECT:{{left={0},top={1},right={2},bottom={3}}}", Left.ToString(), Top.ToString(), Right.ToString(), Bottom.ToString());
            }
        }
        
    }
}
