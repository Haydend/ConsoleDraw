using ConsoleDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            WindowManager.UpdateWindow(100, 40);
            WindowManager.UpdateWindow(100, 40);
            
            WindowManager.SetWindowTitle("TEXT");

            //Start Program
            new MainWindow();  
 
            //Anything to run before exit
        }
    }
}
