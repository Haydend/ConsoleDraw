using ConsoleDraw.Inputs.Base;
using ConsoleDraw.Windows;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Inputs
{
    public class FileSelect : Input
    {
        public String CurrentPath { get; private set; }
        public String CurrentlySelectedFile { get; private set; }
        private List<String> FileNames = new List<String>();
        private List<String> Folders;

        public bool IncludeFiles;
        public String FilterByExtension = "*";

        private ConsoleColor BackgroundColour = ConsoleColor.DarkGray;
        private ConsoleColor TextColour = ConsoleColor.Black;
        private ConsoleColor SelectedTextColour = ConsoleColor.White;
        private ConsoleColor SelectedBackgroundColour = ConsoleColor.Gray;

        private int Width = 56;
        private int Height = 13;

        private int cursorX;
        private int CursorX { get { return cursorX; } set { cursorX = value; GetCurrentlySelectedFileName(); SetOffset(); } }

        private int Offset = 0;
        private bool Selected = false;
        private int Skip = 0;

        public Action ChangeItem;
        public Action SelectFile;

        public FileSelect(int x, int y, String path, String iD, Window parentWindow, bool includeFiles = false, string filterByExtension = "*")
            : base(parentWindow, iD)
        {
            Xpostion = x;
            Ypostion = y;
 
            CurrentPath = path;
            CurrentlySelectedFile = "";
            IncludeFiles = includeFiles;
            FilterByExtension = filterByExtension;

            GetFileNames();
            Selectable = true;
        }

        
        public override void Draw()
        { 
            WindowManager.DrawColourBlock(BackgroundColour, Xpostion, Ypostion, Xpostion + Height, Ypostion + Width);

            var trimedPath = CurrentPath.PadRight(Width - 2, ' ');
            trimedPath = trimedPath.Substring(trimedPath.Count() - Width + 2, Width - 2);

            WindowManager.WirteText(trimedPath, Xpostion, Ypostion + 1, ConsoleColor.Gray, BackgroundColour);

            var i = Offset;
            while (i < Math.Min(Folders.Count, Height + Offset - 1))
            { 
                if(i == CursorX)
                    if(Selected)
                        WindowManager.WirteText(Folders[i], Xpostion + i - Offset + 1, Ypostion + 1, SelectedTextColour, SelectedBackgroundColour);
                    else
                        WindowManager.WirteText(Folders[i], Xpostion + i - Offset + 1, Ypostion + 1, SelectedTextColour, BackgroundColour);
                else
                    WindowManager.WirteText(Folders[i], Xpostion + i - Offset + 1, Ypostion + 1, TextColour, BackgroundColour);

                i++;
            }

            while (i < Math.Min(Folders.Count + FileNames.Count, Height + Offset - 1))
            {
                if (i == CursorX)
                    if (Selected)
                        WindowManager.WirteText(FileNames[i - Folders.Count], Xpostion + i - Offset + 1, Ypostion + 1, SelectedTextColour, SelectedBackgroundColour);
                    else
                        WindowManager.WirteText(FileNames[i - Folders.Count], Xpostion + i - Offset + 1, Ypostion + 1, SelectedTextColour, BackgroundColour);
                else
                    WindowManager.WirteText(FileNames[i - Folders.Count], Xpostion + i - Offset + 1, Ypostion + 1, TextColour, BackgroundColour);
                i++;
            }   

        }

        public void GetFileNames()
        {
            try
            {
                if(IncludeFiles)
                    FileNames = Directory.GetFiles(CurrentPath, "*." + FilterByExtension).Select(path => System.IO.Path.GetFileName(path)).ToList();

                Folders = Directory.GetDirectories(CurrentPath).Select(path => System.IO.Path.GetFileName(path)).ToList();

                if (Directory.GetParent(CurrentPath) != null)
                {
                    Skip = 1;
                    Folders.Insert(0, "..");
                }
                else
                    Skip = 0;

                if (CursorX > FileNames.Count() + Folders.Count())
                    CursorX = 0;
            }
            catch (Exception e)
            {
                throw e;
            }
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

        public override void CursorMoveDown()
        {
            if (CursorX != Folders.Count + FileNames.Count - 1)
                CursorX++;
            else
                CursorX = 0;

            Draw();
        }

        public override void CursorMoveUp()
        {
            if (CursorX != 0)
                CursorX--;
            else
                CursorX = Folders.Count + FileNames.Count - 1;

                Draw();
            
        }

        public override void CursorMoveRight()
        {
            if (CursorX >= Skip && CursorX < Folders.Count) //Folder is selected
                GoIntoFolder();
        } 

        public override void Enter()
        {
            if (CursorX >= Skip && CursorX < Folders.Count) //Folder is selected
                GoIntoFolder();
            else if (cursorX == 0 && Skip == 1) //Back is selected
                GoToParentFolder();
            else if (SelectFile != null) //File is selcted
                SelectFile();
            
        }

        private void GoIntoFolder()
        {
            CurrentPath = Path.Combine(CurrentPath, Folders[cursorX]);
            try
            {
                GetFileNames();
                CursorX = 0;
                Draw();
            }
            catch
            {
                CurrentPath = Directory.GetParent(CurrentPath).FullName; //Change Path back to parent
                new Alert(ParentWindow, "Ouch! An error has occured, Sorry :(");
            }
        }

        public override void CursorMoveLeft()
        {
            if(Skip == 1)
                GoToParentFolder();
        }

        public override void BackSpace()
        {
            if (Skip == 1)
                GoToParentFolder();
        }

        private void GoToParentFolder()
        {
            CurrentPath = Directory.GetParent(CurrentPath).FullName;
            GetFileNames();
            CursorX = 0;
            Draw();
        }

        private void SetOffset()
        {
            while (CursorX - Offset > Height - 2)
                Offset++;

            while (CursorX - Offset < 0)
                Offset--;
        }

        private void GetCurrentlySelectedFileName()
        {
            if (cursorX >= Folders.Count()) //File is selected
            {
                CurrentlySelectedFile = FileNames[cursorX - Folders.Count];
                if (ChangeItem != null)
                    ChangeItem();
            }
            else
            {
                if (CurrentlySelectedFile != "")
                {
                    CurrentlySelectedFile = "";
                    if (ChangeItem != null)
                        ChangeItem();
                }
            }
        }
    }
}
