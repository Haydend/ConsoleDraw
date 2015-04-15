using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Inputs
{
    public class Label : Input
    {
        private String Text = "";
        private ConsoleColor TextColour = ConsoleColor.Black;
        private ConsoleColor BackgroudColour = ConsoleColor.Gray;

        public Label(String text, int x, int y, String iD, Window parentWindow) : base(parentWindow, iD)
        {
            Text = text;
            Xpostion = x;
            Ypostion = y;

            Selectable = false;
        }

        public override void Draw()
        {
            WindowManager.WirteText(Text, Xpostion, Ypostion, TextColour, BackgroudColour);
        }

        public void SetText(String text)
        {
            Text = text;
            Draw();
        }
       
    }
}
