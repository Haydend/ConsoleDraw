using System;

namespace ConsoleDraw.Windows.Base
{
    public class FullWindow : Window
    {


        public FullWindow(int postionX, int postionY, int width, int height, Window parentWindow)
            : base(postionX, postionY, width, height, parentWindow)
        {
            BackgroundColour = ConsoleColor.Gray;
        }

        public override void ReDraw()
        {
            WindowManager.DrawColourBlock(BackgroundColour, PostionX, PostionY, PostionX + Height, PostionY + Width); //Main Box
        }

    }
}
