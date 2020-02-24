using ConsoleDraw;
using System;
using TestAppCore.Windows;

namespace TestAppCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            WindowManager.UpdateWindow(100, 48);
            WindowManager.SetWindowTitle("Test App");

            new MainWindow();
        }
    }
}
