using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;

namespace ConsoleDraw.Inputs
{
    public class ProgressBar : Input
    {
        public ConsoleColor BackgroundColour = ConsoleColor.Gray;
        public ConsoleColor BarColour = ConsoleColor.Black;

        public int PercentageComplete = 50; 

        public ProgressBar(int x, int y, int height, int width, String iD, Window parentWindow) : base(x, y, height, width, parentWindow, iD)
        {
            Selectable = false;
        }

        public override void Draw()
        {
            int widthCompleted = (int)Math.Round(Width * ((double)PercentageComplete / 100));
            int widthUncompleted = Width - widthCompleted;

            //WindowManager.DrawColourBlock(BackgroundColour, Xpostion, Ypostion, Xpostion + Height, Ypostion + Width);
            WindowManager.WirteText("".PadRight(widthCompleted, '█'), Xpostion, Ypostion, BarColour, BackgroundColour);
            WindowManager.WirteText("".PadRight(widthUncompleted, '▒'), Xpostion, Ypostion + widthCompleted, BarColour, BackgroundColour);
        }

    }
}
