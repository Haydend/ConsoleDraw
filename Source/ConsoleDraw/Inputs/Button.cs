using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class Button : Input
    {
        private string Text;
        public ConsoleColor BackgroundColour = ConsoleColor.Gray;
        private ConsoleColor TextColour = ConsoleColor.Black;

        private ConsoleColor SelectedBackgroundColour = ConsoleColor.DarkGray;
        private ConsoleColor SelectedTextColour = ConsoleColor.White;

        private bool Selected = false;

        public Action Action;

        public Button(Window parentWindow, int x, int y, string text, string iD) : base(parentWindow, x, y, 1, text.Count() + 2, iD)
        {
            Text = text;
            BackgroundColour = parentWindow.BackgroundColour;
            Selectable = true;
        }

        public override void Select()
        {
            if (!Selected)
            {
                Selected = true;
                Draw();
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

        public override void Enter()
        {
            Action?.Invoke();
        }

        public override void Draw()
        {
            if (Selected)
                WindowManager.WriteText('[' + Text + ']', Xpostion, Ypostion, SelectedTextColour, SelectedBackgroundColour);
            else
                WindowManager.WriteText('[' + Text + ']', Xpostion, Ypostion, TextColour, BackgroundColour);
        }

        public override void CursorMoveDown()
        {
            ParentWindow.MovetoNextItemDown(Xpostion, Ypostion, Width);
        }

        public override void CursorMoveRight()
        {
            ParentWindow.MovetoNextItemRight(Xpostion - 1, Ypostion + Width, 3);

        }

        public override void CursorMoveLeft()
        {
            ParentWindow.MovetoNextItemLeft(Xpostion - 1, Ypostion, 3);
        }

        public override void CursorMoveUp()
        {
            ParentWindow.MovetoNextItemUp(Xpostion, Ypostion, Width);
        }
    }
}
