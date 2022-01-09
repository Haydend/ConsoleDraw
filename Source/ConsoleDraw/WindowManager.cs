using System;

namespace ConsoleDraw
{
    public static class WindowManager
    {
        public static void DrawColourBlock(ConsoleColor colour, int startX, int startY, int endX, int endY)
        {
            Console.BackgroundColor = colour;

            for (int i = startX; i < endX; i++)
            {
                Console.CursorLeft = startY;
                Console.CursorTop = i;

                Console.WriteLine("".PadLeft(endY - startY));
            }
        }

        public static void WriteText(string text, int startX, int startY, ConsoleColor textColour, ConsoleColor backgroundColour)
        {
            Console.CursorLeft = startY;
            Console.CursorTop = startX;

            Console.BackgroundColor = backgroundColour;
            Console.ForegroundColor = textColour;

            Console.Write(text);

        }


        private static int startingX;
        private static int startingY;
        private static ConsoleColor startingForegroundColour;
        private static ConsoleColor startingBackgroundColour;
        private static int startingBufferHeight;
        private static int startingBufferWidth;

        public static void SetupWindow()
        {
            startingBufferHeight = Console.BufferHeight;
            startingBufferWidth = Console.BufferWidth;

            int whereToMove = Console.CursorTop + 1; //Move one line below visible
            if (whereToMove < Console.WindowHeight) //If cursor is not on bottom line of visible
                whereToMove = Console.WindowHeight + 1;

            if (Console.BufferHeight < whereToMove + Console.WindowHeight) //Buffer is too small
                Console.BufferHeight = whereToMove + Console.WindowHeight;

            Console.MoveBufferArea(0, 0, Console.WindowWidth, Console.WindowHeight, 0, whereToMove);

            Console.CursorVisible = false;
            startingX = Console.CursorTop;
            startingY = Console.CursorLeft;
            startingForegroundColour = Console.ForegroundColor;
            startingBackgroundColour = Console.BackgroundColor;

            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }

        public static void EndWindow()
        {
            Console.ForegroundColor = startingForegroundColour;
            Console.BackgroundColor = startingBackgroundColour;

            int whereToGet = startingX + 1; //Move one line below visible
            if (whereToGet < Console.WindowHeight) //If cursor is not on bottom line of visible
                whereToGet = Console.WindowHeight + 1;
            Console.MoveBufferArea(0, whereToGet, Console.WindowWidth, Console.WindowHeight, 0, 0);

            Console.CursorTop = startingX;
            Console.CursorLeft = startingY;

            Console.CursorVisible = true;
            Console.BufferHeight = startingBufferHeight;
            Console.BufferWidth = startingBufferWidth;
            //Console.WriteLine();

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
            WindowManager.DrawColourBlock(Console.BackgroundColor, 0, 0, Console.BufferHeight, Console.BufferWidth); //Flush Buffer
        }

        public static void SetWindowTitle(string title)
        {
            Console.Title = title;
        }
    }
}
