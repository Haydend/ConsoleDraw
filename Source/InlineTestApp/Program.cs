using ConsoleDraw;
using ConsoleDraw.Windows;
using System;

namespace InlineTestApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Test App");

            Console.Write("Loading Alert Message: ");

            WindowManager.SetupWindow();
            _ = new Alert(null, "This is an alert!");
            WindowManager.EndWindow();
            Console.WriteLine("Success");

            Console.Write("Loading Confirm Message, result: ");
            WindowManager.SetupWindow();
            Confirm confirm = new(null, "Are you wish to run this program?");
            DialogResult result = confirm.ShowDialog();          
            WindowManager.EndWindow();
            Console.WriteLine(result.ToString());

            Console.WriteLine("Test Complete...");
            Console.ReadLine();
        }
    }
}
