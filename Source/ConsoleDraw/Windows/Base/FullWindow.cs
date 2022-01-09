using System;

namespace ConsoleDraw.Windows.Base
{
    public class FullWindow : Window
    {


        public FullWindow(Window parentWindow, int postionX, int postionY, int width, int height)
            : base(parentWindow, postionX, postionY, width, height)
        {
            BackgroundColour = ConsoleColor.Gray;
        }

        public override void ReDraw()
        {
            WindowManager.DrawColourBlock(BackgroundColour, PostionX, PostionY, PostionX + Height, PostionY + Width); //Main Box
        }

    }
}
