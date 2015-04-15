using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Inputs
{
    public class DropdownItem : Input
    {
        public String Text = "";
        private ConsoleColor TextColour = ConsoleColor.White;
        private ConsoleColor BackgroudColour = ConsoleColor.DarkGray;
        private ConsoleColor SelectedTextColour = ConsoleColor.Black;
        private ConsoleColor SelectedBackgroundColour = ConsoleColor.Gray;

        private bool Selected = false;
        public Action Action;

        public DropdownItem(String text, int x, String iD, Window parentWindow) : base(parentWindow, iD)
        {
            Text = text;
            Xpostion = x; // Not Used
            Ypostion = 0; // Not Used

            Selectable = true;
        }

        public override void Draw()
        {
            var paddedText = (Text).PadRight(ParentWindow.Width - 2, ' ');

            if (Selected)
                WindowManager.WirteText(paddedText, Xpostion, ParentWindow.PostionY + 1, SelectedTextColour, SelectedBackgroundColour);
            else
                WindowManager.WirteText(paddedText, Xpostion, ParentWindow.PostionY + 1, TextColour, BackgroudColour);
        }

        public override void Select()
        {
            if (!Selected)
            {
                Selected = true;
                Draw();

                if (Action != null)
                    Action();
            }
        }

        public override void Unselect()
        {
            if (Selected)
            {
                Selected = false;
                Draw();
            }
        }

        public override void BackSpace()
        {
            //ParentWindow.ParentWindow.MoveToNextItem();
            ParentWindow.SelectFirstItem();
            ParentWindow.ExitWindow();
        }

        public override void CursorMoveDown()
        {
            
            ParentWindow.MoveToNextItem();
        }
        public override void CursorMoveUp()
        {
            ParentWindow.MoveToLastItem();
        }

        public override void CursorMoveRight()
        {
            ParentWindow.ExitWindow();
            ParentWindow.ParentWindow.MoveToNextItem();
        }

        public override void CursorMoveLeft()
        {
            ParentWindow.ExitWindow();
            ParentWindow.ParentWindow.MoveToLastItem();
        }
    }
}
