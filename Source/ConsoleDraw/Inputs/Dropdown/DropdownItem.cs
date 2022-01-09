using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;

namespace ConsoleDraw.Inputs
{
    public class DropdownItem : Input
    {
        public string Text = "";
        private ConsoleColor TextColour = ConsoleColor.White;
        private ConsoleColor BackgroudColour = ConsoleColor.DarkGray;
        private ConsoleColor SelectedTextColour = ConsoleColor.Black;
        private ConsoleColor SelectedBackgroundColour = ConsoleColor.Gray;

        private bool Selected = false;
        public Action Action;

        public DropdownItem(string text, int x, string iD, Window parentWindow) : base(x, parentWindow.PostionY + 1, 1, parentWindow.Width - 2, parentWindow, iD)
        {
            Text = text;

            Selectable = true;
        }

        public override void Draw()
        {
            string paddedText = (Text).PadRight(Width, ' ');

            if (Selected)
                WindowManager.WriteText(paddedText, Xpostion, Ypostion, SelectedTextColour, SelectedBackgroundColour);
            else
                WindowManager.WriteText(paddedText, Xpostion, Ypostion, TextColour, BackgroudColour);
        }

        public override void Select()
        {
            if (!Selected)
            {
                Selected = true;
                Draw();

                Action?.Invoke();
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
