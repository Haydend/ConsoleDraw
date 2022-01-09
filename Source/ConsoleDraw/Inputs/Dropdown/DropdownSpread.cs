using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class DropdownSpread : FullWindow
    {
        private List<DropdownItem> DropdownItems = new();
        public Dropdown root;

        public DropdownSpread(int Xpostion, int Ypostion, List<string> options, Window parentWindow, Dropdown root)
            : base(Xpostion, Ypostion, 20, options.Count(), parentWindow)
        {
            for (int i = 0; i < options.Count(); i++)
            {
                DropdownItem item = new(options[i], Xpostion + i, "option" + i, this)
                {
                    Action = delegate ()
                    {
                        root.Text = ((DropdownItem)CurrentlySelected).Text;
                        root.Draw();
                    }
                };

                DropdownItems.Add(item);
            }

            Inputs.AddRange(DropdownItems);

            CurrentlySelected = DropdownItems.FirstOrDefault(x => x.Text == root.Text);

            BackgroundColour = ConsoleColor.DarkGray;
            Draw();
            MainLoop();
        }
    }
}
