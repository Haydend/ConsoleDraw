using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class Label : Input
    {
        private string Text = "";
        private ConsoleColor TextColour = ConsoleColor.Black;
        public ConsoleColor BackgroundColour = ConsoleColor.Gray;

        public Label(string text, int x, int y, string iD, Window parentWindow) : base(x, y, 1, text.Count(), parentWindow, iD)
        {
            Text = text;
            BackgroundColour = parentWindow.BackgroundColour;
            Selectable = false;
        }

        public override void Draw()
        {
            WindowManager.WriteText(Text, Xpostion, Ypostion, TextColour, BackgroundColour);
        }

        public void SetText(string text)
        {
            Text = text;
            Width = text.Count();
            Draw();
        }

    }
}
