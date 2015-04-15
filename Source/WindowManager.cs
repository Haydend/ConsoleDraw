using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw
{
    public static class WindowManager
    {
        public static void DrawColourBlock(ConsoleColor colour, int startX, int startY, int endX, int endY)
        {
            Console.BackgroundColor = colour;

            for (var i = startX; i < endX; i++)
            {
                Console.CursorLeft = startY;
                Console.CursorTop = i;

                Console.WriteLine("".PadLeft(endY - startY));
            }
        }

        public static void WirteText(String text, int startX, int startY, ConsoleColor textColour, ConsoleColor backgroundColour)
        {
            Console.CursorLeft = startY;
            Console.CursorTop = startX;

            Console.BackgroundColor = backgroundColour;
            Console.ForegroundColor = textColour;

            Console.Write(text);

        }

        public static void UpdateWindow(int width, int height)
        {
            Console.CursorVisible = false;

            if (width > Console.BufferWidth) //new Width is bigger then buffer
            {
                Console.BufferWidth = width;
                Console.WindowWidth = width;
            }
            else
            {
                Console.WindowWidth = width;
                Console.BufferWidth = width;
            }

            if (height > Console.BufferWidth) //new Height is bigger then buffer
            {
                Console.BufferHeight = height;
                Console.WindowHeight = height;
            }
            else
            {
                Console.WindowHeight = height;
                Console.BufferHeight = height;
            }

            Console.BackgroundColor = ConsoleColor.Gray;
            WindowManager.DrawColourBlock(Console.BackgroundColor, 0, 0, Console.WindowHeight, Console.WindowWidth); //Flush Buffer
        }

        public static void SetWindowTitle(String title)
        {
            Console.Title = title;
        }
    }
}
