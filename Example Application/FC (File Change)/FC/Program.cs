using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDraw;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace FC
{
    class Program
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        IntPtr hWnd = FindWindow(null, "Untitled - Notepad");

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, int wParam, int lParam);

        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_CHAR = 0x0102;

        public static IntPtr cmdHwnd = IntPtr.Zero;


        static void Main(string[] args)
        {
            foreach (IntPtr child in GetChildWindows(Process.GetCurrentProcess().MainWindowHandle))
            {
                StringBuilder sb = new StringBuilder(100);
                GetClassName(child, sb, sb.Capacity);

                if (sb.ToString() == "ConsoleWindowClass")
                {
                    cmdHwnd = child;
                }
            }

            




            WindowManager.SetupWindow();
            var mainWindow = new MainWindow();
            WindowManager.EndWindow();

            if (mainWindow.changePath) //User did not cancel
            {
                var command = "chdir " + mainWindow.Path;
                uint wparam = 0 << 29 | 0;
                for (var i = 0; i < command.Length; i++)
                {
                    PostMessage(cmdHwnd, WM_CHAR, (int)command[i], 0);
                    PostMessage(cmdHwnd, WM_KEYDOWN, (IntPtr)ConsoleKey.Applications, (IntPtr)wparam); //Hacky buy fix 
                }
                PostMessage(cmdHwnd, WM_KEYDOWN, (IntPtr)ConsoleKey.Enter, (IntPtr)wparam);
            }

        }

        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            // You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

    }
}
