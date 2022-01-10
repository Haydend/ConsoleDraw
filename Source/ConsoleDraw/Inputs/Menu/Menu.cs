using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class Menu : Input
    {
        private string Text = "";
        private ConsoleColor TextColour = ConsoleColor.Black;
        private ConsoleColor BackgroudColour = ConsoleColor.Gray;
        private ConsoleColor SelectedTextColour = ConsoleColor.White;
        private ConsoleColor SelectedBackgroundColour = ConsoleColor.DarkGray;

        private bool Selected = false;
        public List<MenuItem> MenuItems = new();
        public MenuDropdown MenuDropdown;

        public Menu(Window parentWindow, string text, int x, int y, string iD) : base(parentWindow, x, y, 1, text.Count() + 2, iD)
        {
            Text = text;
            Xpostion = x;
            Ypostion = y;

            Selectable = true;
        }

        public override void Draw()
        {
            if (Selected)
                WindowManager.WriteText('[' + Text + ']', Xpostion, Ypostion, SelectedTextColour, SelectedBackgroundColour);
            else
                WindowManager.WriteText('[' + Text + ']', Xpostion, Ypostion, TextColour, BackgroudColour);
        }

        public override void Select()
        {
            if (!Selected)
            {
                Selected = true;
                Draw();

                new MenuDropdown(Xpostion + 1, Ypostion, MenuItems, ParentWindow);

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
            MenuDropdown = new MenuDropdown(Xpostion + 1, Ypostion, MenuItems, ParentWindow);
        }

        public override void CursorMoveLeft()
        {
            ParentWindow.MoveToLastItem();
        }
        public override void CursorMoveRight()
        {
            ParentWindow.MoveToNextItem();
        }

        public override void CursorMoveDown()
        {
            MenuDropdown = new MenuDropdown(Xpostion + 1, Ypostion, MenuItems, ParentWindow);
        }
    }
}
