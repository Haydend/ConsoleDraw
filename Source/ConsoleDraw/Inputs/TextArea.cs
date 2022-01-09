using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDraw.Inputs
{
    public class TextArea : Input
    {
        private bool Selected = false;

        private int CursorPostion;

        private int cursorDisplayX;
        private int CursorDisplayX { get => cursorDisplayX; set { cursorDisplayX = value; SetOffset(); } }

        private int CursorDisplayY;

        private int Offset = 0;
        private List<string> SplitText = new();
        private string text = "";
        private string Text
        {
            get => text;
            set
            {
                if (OnChange != null && text != value)
                    OnChange();

                text = value;

                SplitText = CreateSplitText();
            }
        }
        private string TextWithoutNewLine => RemoveNewLine(Text);

        private ConsoleColor TextColour = ConsoleColor.White;
        public ConsoleColor BackgroundColour = ConsoleColor.Blue;

        private Cursor cursor = new();

        public Action OnChange;

        public TextArea(int x, int y, int width, int height, string iD, Window parentWindow) : base(x, y, height, width, parentWindow, iD)
        {
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

        public override void AddLetter(char letter)
        {
            string textBefore = Text[..CursorPostion];
            string textAfter = Text[CursorPostion..];

            Text = textBefore + letter + textAfter;

            CursorPostion++;
            Draw();
        }

        public override void CursorMoveLeft()
        {
            if (CursorPostion != 0)
                CursorPostion--;

            Draw();
        }

        public override void CursorMoveRight()
        {
            if (CursorPostion != Text.Length)
            {
                CursorPostion++;
                Draw();
            }
        }

        public override void CursorMoveDown()
        {
            List<string> splitText = SplitText;

            if (splitText.Count == CursorDisplayX + 1 || splitText.Count == 0) //Cursor at end of text in text area
            {
                ParentWindow.MovetoNextItemDown(Xpostion, Ypostion, Width);
                return;
            }

            string nextLine = splitText[CursorDisplayX + 1];

            int newCursor = 0;
            for (int i = 0; i < cursorDisplayX + 1; i++)
            {
                newCursor += splitText[i].Count();
            }

            if (nextLine.Count() > CursorDisplayY)
                newCursor += CursorDisplayY;
            else
                newCursor += nextLine.Where(x => x != '\n').Count();


            CursorPostion = newCursor;

            Draw();
        }

        public override void CursorMoveUp()
        {
            List<string> splitText = SplitText;

            if (0 == CursorDisplayX) //Cursor at top of text area
            {
                ParentWindow.MovetoNextItemUp(Xpostion, Ypostion, Width);
                return;
            }

            string nextLine = splitText[CursorDisplayX - 1];

            int newCursor = 0;
            for (int i = 0; i < cursorDisplayX - 1; i++)
            {
                newCursor += splitText[i].Count();
            }

            if (nextLine.Count() >= CursorDisplayY)
                newCursor += CursorDisplayY;
            else
                newCursor += nextLine.Where(x => x != '\n').Count();

            CursorPostion = newCursor;
            Draw();
        }

        public override void CursorToStart()
        {
            List<string> splitText = SplitText;

            int newCursor = 0;
            for (int i = 0; i < cursorDisplayX; i++)
            {
                newCursor += splitText[i].Count();
            }

            CursorPostion = newCursor;
            Draw();
        }

        public override void CursorToEnd()
        {
            List<string> splitText = SplitText;
            string currentLine = splitText[cursorDisplayX];

            int newCursor = 0;
            for (int i = 0; i < cursorDisplayX + 1; i++)
            {
                newCursor += splitText[i].Count();
            }

            CursorPostion = newCursor - currentLine.Count(x => x == '\n');
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

        public override void Enter()
        {
            AddLetter('\n');
        }

        public void SetText(string text)
        {
            Text = text;
            CursorPostion = 0;
            Draw();
        }

        public string GetText()
        {
            return Text;
        }

        public override void Draw()
        {
            RemoveCursor();

            UpdateCursorDisplayPostion();

            List<string> lines = SplitText;

            //Draw test area
            for (int i = Offset; i < Height + Offset; i++)
            {
                string line = ' ' + "".PadRight(Width - 1, ' ');
                if (lines.Count > i)
                    line = ' ' + RemoveNewLine(lines[i]).PadRight(Width - 1, ' ');

                WindowManager.WriteText(line, i + Xpostion - Offset, Ypostion, TextColour, BackgroundColour);
            }

            if (Selected)
                ShowCursor();

            //Draw Scroll Bar
            WindowManager.DrawColourBlock(ConsoleColor.White, Xpostion, Ypostion + Width, Xpostion + Height, Ypostion + Width + 1);

            double linesPerPixel = (double)lines.Count() / (Height);
            int postion = 0;
            if (linesPerPixel > 0)
                postion = (int)Math.Floor(cursorDisplayX / linesPerPixel);

            WindowManager.WriteText("■", Xpostion + postion, Ypostion + Width, ConsoleColor.DarkGray, ConsoleColor.White);
        }

        private List<string> CreateSplitText()
        {
            List<string> splitText = new();

            int lastSplit = 0;
            for (int i = 0; i < Text.Count() + 1; i++)
            {
                if (Text.Count() > i && Text[i] == '\n')
                {
                    splitText.Add(Text.Substring(lastSplit, i - lastSplit + 1));
                    lastSplit = i + 1;
                }
                else if (i - lastSplit == Width - 2)
                {
                    splitText.Add(Text[lastSplit..i]);
                    lastSplit = i;
                }

                if (i == Text.Count())
                    splitText.Add(Text[lastSplit..Text.Count()]);
            }

            return splitText.Select(x => x.Replace('\r', ' ')).ToList();
        }

        private void ShowCursor()
        {
            cursor.PlaceCursor(Xpostion + CursorDisplayX - Offset, Ypostion + 1 + CursorDisplayY, (Text + ' ')[CursorPostion], BackgroundColour);
        }

        private void UpdateCursorDisplayPostion()
        {
            List<string> lines = SplitText;
            int displayX = 0;
            int displayY = 0;

            for (int i = 0; i < CursorPostion; i++)
            {
                if (lines[displayX].Count() > displayY && lines[displayX][displayY] == '\n') //Skip NewLine characters
                {
                    displayY++;
                }

                if (lines.Count > displayX)
                {
                    if (lines[displayX].Count() > displayY)
                        displayY++;
                    else if (lines.Count - 1 > displayX)
                    {
                        displayX++;
                        displayY = 0;
                    }

                }

                if (displayY == 0 && displayX - 1 >= 0 && lines[displayX - 1].Last() != '\n') //Wordwrap Stuff
                {
                    displayY++;
                }
                else if (displayY == 1 && displayX - 1 >= 0 && lines[displayX - 1].Last() != '\n')
                {
                    displayY--;
                }

            }

            CursorDisplayX = displayX;
            CursorDisplayY = displayY;
        }

        private void RemoveCursor()
        {
            cursor.RemoveCursor();
        }

        private void SetOffset()
        {
            while (CursorDisplayX - Offset > Height - 1)
                Offset++;

            while (CursorDisplayX - Offset < 0)
                Offset--;
        }

        private string RemoveNewLine(string text)
        {
            string toReturn = "";

            foreach (char letter in text)
            {
                if (letter != '\n')
                    toReturn += letter;
            }

            return toReturn;
        }
    }
}
