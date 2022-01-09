using ConsoleDraw.Inputs.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDraw.Windows.Base
{
    public class Window : IWindow
    {
        public Boolean Exit;
        protected IInput CurrentlySelected;

        public int PostionX { get; private set; }
        public int PostionY { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ConsoleColor BackgroundColour = ConsoleColor.Gray;

        public List<IInput> Inputs = new List<IInput>();

        public Window(int postionX, int postionY, int width, int height, Window parentWindow)
        {
            PostionY = postionY;
            PostionX = postionX;
            Width = width;
            Height = height;

            ParentWindow = parentWindow;
        }

        public void Draw()
        {
            if (ParentWindow != null)
                ParentWindow.Draw();

            ReDraw();

            foreach (IInput input in Inputs)
                input.Draw();

            if (CurrentlySelected != null)
                CurrentlySelected.Select();
            // SetSelected();
        }

        public override void ReDraw()
        {

        }


        public void MainLoop()
        {
            while (!Exit && !ProgramInfo.ExitProgram)
            {
                ConsoleKeyInfo input = ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Tab:
                        CurrentlySelected.Tab();
                        break;
                    case ConsoleKey.Enter:
                        CurrentlySelected.Enter();
                        break;
                    case ConsoleKey.LeftArrow:
                        CurrentlySelected.CursorMoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        CurrentlySelected.CursorMoveRight();
                        break;
                    case ConsoleKey.UpArrow:
                        CurrentlySelected.CursorMoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        CurrentlySelected.CursorMoveDown();
                        break;
                    case ConsoleKey.Backspace:
                        CurrentlySelected.BackSpace();
                        break;
                    case ConsoleKey.Home:
                        CurrentlySelected.CursorToStart();
                        break;
                    case ConsoleKey.End:
                        CurrentlySelected.CursorToEnd();
                        break;
                    default:
                        CurrentlySelected.AddLetter((Char)input.KeyChar); // Letter(input.KeyChar);
                        break;
                } // Letter(input.KeyChar);
            }
        }

        public void SelectFirstItem()
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            CurrentlySelected = Inputs.First(x => x.Selectable);

            SetSelected();
        }

        public void SelectItemByID(String Id)
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            IInput newSelectedInput = Inputs.FirstOrDefault(x => x.ID == Id);
            if (newSelectedInput == null) //No Input with this ID
                return;

            CurrentlySelected = newSelectedInput;

            SetSelected();
        }

        public void MoveToNextItem()
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            if (Inputs.Count(x => x.Selectable) == 1) //Only one selectable input on page, thus no point chnaging it
                return;

            int IndexOfCurrent = Inputs.IndexOf(CurrentlySelected);

            while (true)
            {
                IndexOfCurrent = MoveIndexAlongOne(IndexOfCurrent);

                if (Inputs[IndexOfCurrent].Selectable)
                    break;
            }
            CurrentlySelected = Inputs[IndexOfCurrent];

            SetSelected();
        }

        public void MoveToLastItem()
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            if (Inputs.Count(x => x.Selectable) == 1) //Only one selectable input on page, thus no point chnaging it
                return;

            int IndexOfCurrent = Inputs.IndexOf(CurrentlySelected);

            while (true)
            {
                IndexOfCurrent = MoveIndexBackOne(IndexOfCurrent);

                if (Inputs[IndexOfCurrent].Selectable)
                    break;
            }
            CurrentlySelected = Inputs[IndexOfCurrent];

            SetSelected();
        }

        public void MovetoNextItemRight(int startX, int startY, int searchHeight)
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            if (Inputs.Count(x => x.Selectable) == 1) //Only one selectable input on page, thus no point chnaging it
                return;

            IInput nextItem = null;
            while (nextItem == null && startY <= PostionY + Width)
            {
                foreach (IInput input in Inputs.Where(x => x.Selectable && x != CurrentlySelected))
                {
                    bool overlap = DoAreasOverlap(startX, startY, searchHeight, 1, input.Xpostion, input.Ypostion, input.Height, input.Width);
                    if (overlap)
                    {
                        nextItem = input;
                        break; //end foreach 
                    }
                }
                startY++;
            }

            if (nextItem == null) //No element found to the right
            {
                MoveToNextItem();
                return;
            }

            CurrentlySelected = nextItem;
            SetSelected();
        }

        public void MovetoNextItemLeft(int startX, int startY, int searchHeight)
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            if (Inputs.Count(x => x.Selectable) == 1) //Only one selectable input on page, thus no point chnaging it
                return;

            IInput nextItem = null;
            while (nextItem == null && startY > PostionY)
            {
                foreach (IInput input in Inputs.Where(x => x.Selectable && x != CurrentlySelected))
                {
                    bool overlap = DoAreasOverlap(startX, startY - 1, searchHeight, 1, input.Xpostion, input.Ypostion, input.Height, input.Width);
                    if (overlap)
                    {
                        nextItem = input;
                        break; //end foreach 
                    }
                }
                startY--;
            }

            if (nextItem == null) //No element found
            {
                MoveToLastItem();
                return;
            }

            CurrentlySelected = nextItem;
            SetSelected();
        }

        public void MovetoNextItemDown(int startX, int startY, int searchWidth)
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            if (Inputs.Count(x => x.Selectable) == 1) //Only one selectable input on page, thus no point chnaging it
                return;

            IInput nextItem = null;
            while (nextItem == null && startX <= PostionX + Height)
            {
                foreach (IInput input in Inputs.Where(x => x.Selectable && x != CurrentlySelected))
                {
                    bool overlap = DoAreasOverlap(startX, startY, 1, searchWidth, input.Xpostion, input.Ypostion, input.Height, input.Width);
                    if (overlap)
                    {
                        nextItem = input;
                        break; //end foreach 
                    }
                }
                startX++;
            }

            if (nextItem == null) //No element found
            {
                MoveToNextItem();
                return;
            }

            CurrentlySelected = nextItem;
            SetSelected();
        }

        public void MovetoNextItemUp(int startX, int startY, int searchWidth)
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;

            if (Inputs.Count(x => x.Selectable) == 1) //Only one selectable input on page, thus no point chnaging it
                return;

            IInput nextItem = null;
            while (nextItem == null && startX > PostionX)
            {
                foreach (IInput input in Inputs.Where(x => x.Selectable && x != CurrentlySelected))
                {
                    bool overlap = DoAreasOverlap(startX - 1, startY, 1, searchWidth, input.Xpostion, input.Ypostion, input.Height, input.Width);
                    if (overlap)
                    {
                        nextItem = input;
                        break; //end foreach 
                    }
                }
                startX--;
            }

            if (nextItem == null) //No element found
            {
                MoveToLastItem();
                return;
            }

            CurrentlySelected = nextItem;
            SetSelected();
        }


        private bool DoAreasOverlap(int areaOneX, int areaOneY, int areaOneHeight, int areaOneWidth, int areaTwoX, int areaTwoY, int areaTwoHeight, int areaTwoWidth)
        {
            int areaOneEndX = areaOneX + areaOneHeight - 1;
            int areaOneEndY = areaOneY + areaOneWidth - 1;
            int areaTwoEndX = areaTwoX + areaTwoHeight - 1;
            int areaTwoEndY = areaTwoY + areaTwoWidth - 1;

            bool overlapsVertically = false;
            //Check if overlap vertically
            if (areaOneX >= areaTwoX && areaOneX < areaTwoEndX) //areaOne starts in areaTwo
                overlapsVertically = true;
            else if (areaOneEndX >= areaTwoX && areaOneEndX <= areaTwoEndX) //areaOne ends in areaTwo
                overlapsVertically = true;
            else if (areaOneX < areaTwoX && areaOneEndX >= areaTwoEndX) //areaOne start before and end after areaTwo
                overlapsVertically = true;
            //areaOne inside areaTwo is caught by first two statements

            if (!overlapsVertically) //If it does not overlap vertically, then it does not overlap.
                return false;

            bool overlapsHorizontally = false;
            //Check if overlap Horizontally
            if (areaOneY >= areaTwoY && areaOneY < areaTwoEndY) //areaOne starts in areaTwo
                overlapsHorizontally = true;
            else if (areaOneEndY >= areaTwoY && areaOneEndY < areaTwoEndY) //areaOne ends in areaTwo
                overlapsHorizontally = true;
            else if (areaOneY <= areaTwoY && areaOneEndY >= areaTwoEndY) //areaOne starts before and ends after areaTwo
                overlapsHorizontally = true;
            //areaOne inside areaTwo is caught by first two statements

            if (!overlapsHorizontally) //If it does not overlap Horizontally, then it does not overlap.
                return false;

            return true; //it overlaps vertically and horizontally, thus areas must overlap
        }

        private int MoveIndexAlongOne(int index)
        {
            if (Inputs.Count() == index + 1)
                return 0;

            return index + 1;
        }

        private int MoveIndexBackOne(int index)
        {
            if (index == 0)
                return Inputs.Count() - 1;

            return index - 1;
        }

        private void SetSelected()
        {
            Inputs.ForEach(x => x.Unselect());

            if (CurrentlySelected != null)
                CurrentlySelected.Select();
        }

        private static ConsoleKeyInfo ReadKey()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);

            return input;
        }

        public IInput GetInputById(String Id)
        {
            return Inputs.FirstOrDefault(x => x.ID == Id);
        }

        public void ExitWindow()
        {
            Exit = true;
            if (ParentWindow != null)
                ParentWindow.Draw();
            //else
            //System.Environment.Exit(1);
        }
    }
}
