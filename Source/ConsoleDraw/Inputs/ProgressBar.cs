using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;

namespace ConsoleDraw.Inputs
{
    public class ProgressBar : Input
    {
        public ConsoleColor BackgroundColour = ConsoleColor.Gray;
        public ConsoleColor BarColour = ConsoleColor.Black;

        private int percentageComplete;
        public int PercentageComplete
        {
            get => percentageComplete;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Percentage must be between 0 & 100, actual:{0}", value));
                }
                percentageComplete = value;
                Draw();
            }
        }

        public ProgressBar(int percentageComplete, int x, int y, int height, int width, string iD, Window parentWindow) : base(x, y, height, width, parentWindow, iD)
        {
            Selectable = false;
            PercentageComplete = percentageComplete;
        }

        public override void Draw()
        {
            int widthCompleted = (int)Math.Round(Width * ((double)PercentageComplete / 100));
            int widthUncompleted = Width - widthCompleted;

            //WindowManager.DrawColourBlock(BackgroundColour, Xpostion, Ypostion, Xpostion + Height, Ypostion + Width);


            WindowManager.WriteText("".PadRight(widthCompleted, '█'), Xpostion, Ypostion, BarColour, BackgroundColour);
            WindowManager.WriteText("".PadRight(widthUncompleted, '▒'), Xpostion, Ypostion + widthCompleted, BarColour, BackgroundColour);
        }

    }
}
