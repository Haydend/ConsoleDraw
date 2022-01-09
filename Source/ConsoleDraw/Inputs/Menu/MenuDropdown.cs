using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class MenuDropdown : FullWindow
    {
        private List<MenuItem> MenuItems;

        public MenuDropdown(int Xpostion, int Ypostion, List<MenuItem> menuItems, Window parentWindow)
            : base(parentWindow, Xpostion, Ypostion, 20, menuItems.Count() + 2)
        {

            for (int i = 0; i < menuItems.Count(); i++)
            {
                menuItems[i].ParentWindow = this;
                menuItems[i].Width = Width - 2;
                menuItems[i].Xpostion = Xpostion + i + 1;
                menuItems[i].Ypostion = PostionY + 1;
            }

            MenuItems = menuItems;


            Inputs.AddRange(MenuItems);

            CurrentlySelected = MenuItems.FirstOrDefault();

            BackgroundColour = ConsoleColor.DarkGray;
            Draw();
            MainLoop();
        }


    }
}
