using ConsoleDraw.Inputs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Windows.Base
{
    public class Window : IWindow
    {
        public Boolean Exit;
        protected IInput CurrentlySelected;

        public int PostionX { get; private set; }
        public int PostionY { get; private set; }
        public int Width {get; private set;}
        public int Height { get; private set; }

        protected ConsoleColor BackgroundColour = ConsoleColor.Gray;
        
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

                foreach (var input in Inputs)
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
            while (!Exit)
            {
                var input = ReadKey();

                if (input.Key == ConsoleKey.Tab)
                    CurrentlySelected.Tab();
                else if (input.Key == ConsoleKey.Enter)
                    CurrentlySelected.Enter();
                else if (input.Key == ConsoleKey.LeftArrow)
                    CurrentlySelected.CursorMoveLeft();
                else if (input.Key == ConsoleKey.RightArrow)
                    CurrentlySelected.CursorMoveRight();
                else if (input.Key == ConsoleKey.UpArrow)
                    CurrentlySelected.CursorMoveUp();
                else if (input.Key == ConsoleKey.DownArrow)
                    CurrentlySelected.CursorMoveDown();
                else if (input.Key == ConsoleKey.Backspace)
                    CurrentlySelected.BackSpace();
                else if (input.Key == ConsoleKey.Home)
                    CurrentlySelected.CursorToStart();
                else if (input.Key == ConsoleKey.End)
                    CurrentlySelected.CursorToEnd();
                else
                    CurrentlySelected.AddLetter((Char)input.KeyChar); // Letter(input.KeyChar);
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

            var newSelectedInput = Inputs.FirstOrDefault(x => x.ID == Id);
            if (newSelectedInput == null) //No Input with this ID
                return;

            CurrentlySelected = newSelectedInput;

            SetSelected();
        }

        public void MoveToNextItem()
        {
            if (Inputs.All(x => !x.Selectable)) //No Selectable inputs on page
                return;
            
            var IndexOfCurrent = Inputs.IndexOf(CurrentlySelected);

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

            var IndexOfCurrent = Inputs.IndexOf(CurrentlySelected);

            while (true)
            {
                IndexOfCurrent = MoveIndexBackOne(IndexOfCurrent);

                if (Inputs[IndexOfCurrent].Selectable)
                    break;
            }
            CurrentlySelected = Inputs[IndexOfCurrent];

            SetSelected();
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

            if(CurrentlySelected != null)
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
            else
                System.Environment.Exit(1);
        }
    }
}
