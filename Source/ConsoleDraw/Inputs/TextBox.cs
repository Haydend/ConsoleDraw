using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;

namespace ConsoleDraw.Inputs
{
    public class TextBox : Input
    {
        private bool Selected = false;

        private int cursorPostion;
        private int CursorPostion { get => cursorPostion; set { cursorPostion = value; SetOffset(); } }

        private int Offset = 0;
        private string Text = "";

        private ConsoleColor TextColour = ConsoleColor.White;
        private ConsoleColor BackgroundColour = ConsoleColor.DarkGray;

        private Cursor cursor = new();

        public TextBox(Window parentWindow, int x, int y, string iD, int length = 38) : base(parentWindow, x, y, 1, length, iD)
        {
            Selectable = true;
        }

        public TextBox(Window parentWindow, int x, int y, string text, string iD, int length = 38) : base(parentWindow, x, y, 1, length, iD)
        {
            Text = text;

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

        public override void AddLetter(char letter)
        {
            string textBefore = Text[..CursorPostion];
            string textAfter = Text[CursorPostion..];

            Text = textBefore + letter + textAfter;
            CursorPostion++;
            Draw();
        }

        public override void BackSpace()
        {
            if (CursorPostion != 0)
            {
                string textBefore = Text[..CursorPostion];
                string textAfter = Text[CursorPostion..];

                textBefore = textBefore[0..^1];

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
            else
                ParentWindow.MovetoNextItemLeft(Xpostion - 1, Ypostion, 3);
        }

        public override void CursorMoveRight()
        {
            if (CursorPostion != Text.Length)
            {
                CursorPostion++;
                Draw();
            }
            else
                ParentWindow.MovetoNextItemRight(Xpostion - 1, Ypostion + Width, 3);
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

        public string GetText()
        {
            return Text;
        }

        public void SetText(string text)
        {
            Text = text;
            Draw();
        }

        public override void Draw()
        {
            RemoveCursor();

            string clippedPath = "";

            if (Selected)
                clippedPath = ' ' + Text.PadRight(Width + Offset, ' ').Substring(Offset, Width - 2);
            else
                clippedPath = ' ' + Text.PadRight(Width, ' ')[..(Width - 2)];

            WindowManager.WriteText(clippedPath + " ", Xpostion, Ypostion, TextColour, BackgroundColour);
            if (Selected)
                ShowCursor();
        }

        private void ShowCursor()
        {
            string paddedText = Text + " ";
            cursor.PlaceCursor(Xpostion, Ypostion + CursorPostion - Offset + 1, paddedText[CursorPostion], BackgroundColour);
        }

        private void RemoveCursor()
        {
            cursor.RemoveCursor();
        }

        private void SetOffset()
        {
            while (CursorPostion - Offset > Width - 2)
                Offset++;

            while (CursorPostion - Offset < 0)
                Offset--;
        }



        public override void CursorMoveDown()
        {
            ParentWindow.MovetoNextItemDown(Xpostion, Ypostion, Width);
        }

        public override void CursorMoveUp()
        {
            ParentWindow.MovetoNextItemUp(Xpostion, Ypostion, Width);
        }
    }
}
