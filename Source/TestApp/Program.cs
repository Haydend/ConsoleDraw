using ConsoleDraw;
using TestApp.Windows;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            WindowManager.UpdateWindow(100, 48);
            WindowManager.UpdateWindow(100, 48);
            WindowManager.SetWindowTitle("Test App");

            new MainWindow();
        }
    }
}
