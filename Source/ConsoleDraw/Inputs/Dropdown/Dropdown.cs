using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class Dropdown : Input
    {
        private ConsoleColor TextColour = ConsoleColor.Black;
        private ConsoleColor BackgroudColour = ConsoleColor.Gray;
        private ConsoleColor SelectedTextColour = ConsoleColor.White;
        private ConsoleColor SelectedBackgroundColour = ConsoleColor.DarkGray;

        private bool Selected = false;
        public List<DropdownItem> DropdownItems = new();
        public DropdownSpread DropdownSpread;

        private List<string> Options;
        public string Text;
        public int Length;

        public Action OnUnselect;

        public Dropdown(int x, int y, List<string> options, string iD, Window parentWindow, int length = 20) : base(x, y, 1, length - 2 + 3, parentWindow, iD)
        {
            Xpostion = x;
            Ypostion = y;
            Options = options;
            Text = Options.First();
            Length = length;
            BackgroudColour = parentWindow.BackgroundColour;

            Selectable = true;
        }

        public override void Draw()
        {
            string paddedText = Text.PadRight(Length - 2, ' ')[..(Length - 2)];

            if (Selected)
                WindowManager.WriteText('[' + paddedText + '▼' + ']', Xpostion, Ypostion, SelectedTextColour, SelectedBackgroundColour);
            else
                WindowManager.WriteText('[' + paddedText + '▼' + ']', Xpostion, Ypostion, TextColour, BackgroudColour);
        }

        public override void Select()
        {
            if (!Selected)
            {
                Selected = true;
                Draw();

                new DropdownSpread(Xpostion + 1, Ypostion, Options, ParentWindow, this);
            }
        }

        public override void Unselect()
        {
            if (Selected)
            {
                Selected = false;
                Draw();
                OnUnselect?.Invoke();
            }
        }


    }
}
