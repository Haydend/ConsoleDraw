using ConsoleDraw;
using ConsoleDraw.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test App");

            Console.WriteLine();
            Console.WriteLine();

            for (var i = 0; i < 2; i++ )
                Console.WriteLine(i.ToString());

            Console.WriteLine("Loading Alert Message");
            Console.ReadLine();

            WindowManager.SetupWindow();
            new Alert("This is an alert!", null);
            WindowManager.EndWindow();
            Console.WriteLine("That was the Alert");

            Console.WriteLine("Loading Confirm Message");
            Console.ReadLine();

            WindowManager.SetupWindow();
            var confirm = new Confirm(null, "Are you wish to run this program?");
            WindowManager.EndWindow();

            if (confirm.Result == DialogResult.OK)
                Console.WriteLine("You pressed OK");
            else
                Console.WriteLine("You pressed Cancel");

            Console.ReadLine();
        }
    }
}
