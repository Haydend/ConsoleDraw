using Calculator.Windows;
using ConsoleDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            WindowManager.UpdateWindow(27, 15);
            WindowManager.UpdateWindow(27, 15);
            WindowManager.SetWindowTitle("Calculator");

            new MainWindow();
        }
    }
}
