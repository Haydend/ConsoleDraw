using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Inputs
{
    public class TextBox : Input
    {
        public int Length { get; private set; }

        private bool Selected = false;

        private int cursorPostion;
        private int CursorPostion { get { return cursorPostion; } set { cursorPostion = value; SetOffset(); } }

        private int Offset = 0;
        private String Text = "";

        private ConsoleColor TextColour = ConsoleColor.Black;
        private ConsoleColor BackgroundColour = ConsoleColor.DarkGray;

        private Cursor cursor = new Cursor();

        public TextBox(int x, int y, String iD, Window parentWindow, int length = 38)
            : base(parentWindow, iD)
        {
            Xpostion = x;
            Ypostion = y;
            Length = length;

            Selectable = true;
        }

        public TextBox(int x, int y, String text, String iD, Window parentWindow, int length = 38)
            : base(parentWindow, iD)
        {
            Text = text;
            Xpostion = x;
            Ypostion = y;
            Length = length;
            CursorPostion = text.Length;

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
            ParentWindow.MoveToNextItem();
        }

        public override void AddLetter(Char letter)
        {
            String textBefore = Text.Substring(0, CursorPostion);
            String textAfter = Text.Substring(CursorPostion, Text.Length - CursorPostion);

            Text = textBefore + letter + textAfter;
            CursorPostion++;
            Draw();
        }

        public override void BackSpace()
        {
            if (CursorPostion != 0)
            {
                String textBefore = Text.Substring(0, CursorPostion);
                String textAfter = Text.Substring(CursorPostion, Text.Length - CursorPostion);

                textBefore = textBefore.Substring(0, textBefore.Length - 1);

                Text = textBefore + textAfter;
                CursorPostion--;
                Draw();
            }
        }

        public override void CursorMoveLeft()
        {
            if (CursorPostion != 0)
            {
                CursorPostion--;
                Draw();
            }
        }

        public override void CursorMoveRight()
        {
            if (CursorPostion != Text.Length)
            {
                CursorPostion++;
                Draw();
            }
        }

        public override void CursorToStart()
        {
            CursorPostion = 0;
            Draw();
        }

        public override void CursorToEnd()
        {
            CursorPostion = Text.Length;
            Draw();
        }

        public String GetText()
        {
            return Text;
        }

        public void SetText(String text)
        {
            Text = text;
            Draw();
        }

        public override void Draw()
        {
            RemoveCursor();

            var clippedPath = "";

            if(Selected)
                clippedPath = Text.PadRight(Length + Offset, ' ').Substring(Offset, Length);
            else
                clippedPath = Text.PadRight(Length, ' ').Substring(0, Length);

            WindowManager.WirteText(clippedPath + " ", Xpostion, Ypostion, TextColour, BackgroundColour);
            if (Selected)
                ShowCursor();          
        }

        private void ShowCursor()
        {
            var paddedText = Text + " ";
            cursor.PlaceCursor(Xpostion, Ypostion + CursorPostion - Offset, paddedText[CursorPostion], BackgroundColour);
        }

        private void RemoveCursor()
        {
            cursor.RemoveCursor();
        }

        private void SetOffset()
        {
            while (CursorPostion - Offset > Length)
                Offset++;

            while (CursorPostion - Offset < 0)
                Offset--;
        }



        public override void CursorMoveDown()
        {
            ParentWindow.MoveToNextItem();
        }

        public override void CursorMoveUp()
        {
            ParentWindow.MoveToLastItem();
        }
    }
}
