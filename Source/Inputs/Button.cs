using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Inputs
{
    public class Button : Input
    {
        private String Text;
        private ConsoleColor Colour = ConsoleColor.Gray;
        private ConsoleColor TextColour = ConsoleColor.Black;

        private ConsoleColor SelectedColour = ConsoleColor.DarkGray;
        private ConsoleColor SelectedTextColour = ConsoleColor.White;

        private bool Selected = false;

        public Action Action;

        public Button(int x, int y, String text, String iD, Window parentWindow) : base(parentWindow, iD)
        {
            Xpostion = x;
            Ypostion = y;
            Text = text;

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
            if (Action != null) //If an action has been set
                Action();
        }

        public override void Draw()
        {
            if(Selected)
                WindowManager.WirteText('['+Text+']', Xpostion, Ypostion, SelectedTextColour, SelectedColour);
            else
                WindowManager.WirteText('[' + Text + ']', Xpostion, Ypostion, TextColour, Colour);  
        }
        
        public override void CursorMoveDown()
        {
            ParentWindow.MoveToNextItem();
        }

        public override void CursorMoveRight()
        {
            ParentWindow.MoveToNextItem();
        }

        public override void CursorMoveLeft()
        {
            ParentWindow.MoveToLastItem();
        }

        public override void CursorMoveUp()
        {
            ParentWindow.MoveToLastItem();
        }
    }
}
