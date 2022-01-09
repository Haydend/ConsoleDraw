﻿using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.IO;

namespace ConsoleDraw.Windows
{
    public class SaveMenu : PopupWindow
    {
        private Button saveBtn;
        private Button cancelBtn;
        private TextBox openTxtBox;
        private FileBrowser fileSelect;
        private string Text;

        public bool FileWasSaved;
        public string FileSavedAs;
        public string PathToFile;

        public SaveMenu(Window parentWindow, string fileName, string path, string data)
            : base(parentWindow, "Save Menu", 6, (Console.WindowWidth / 2) - 30, 60, 20)
        {
            BackgroundColour = ConsoleColor.White;
            Text = data;

            fileSelect = new FileBrowser(this, PostionX + 2, PostionY + 2, 56, 12, path, "fileSelect");

            Label openLabel = new(this, "Name", PostionX + 16, PostionY + 2, "openLabel");
            openTxtBox = new TextBox(this, PostionX + 16, PostionY + 7, fileName, "openTxtBox", Width - 13) { Selectable = true };

            saveBtn = new(this, PostionX + 18, PostionY + 2, "Save", "loadBtn")
            {
                Action = delegate () { SaveFile(); }
            };
            cancelBtn = new(this, PostionX + 18, PostionY + 9, "Cancel", "cancelBtn")
            {
                Action = delegate () { ExitWindow(); }
            };

            Inputs.Add(fileSelect);
            Inputs.Add(openLabel);
            Inputs.Add(openTxtBox);
            Inputs.Add(saveBtn);
            Inputs.Add(cancelBtn);

            CurrentlySelected = saveBtn;

            Draw();
            MainLoop();
        }


        private void SaveFile()
        {
            string path = fileSelect.CurrentPath;
            string filename = openTxtBox.GetText();

            string fullFile = Path.Combine(path, filename);

            try
            {
                StreamWriter file = new(fullFile);

                file.Write(Text);

                file.Close();

                FileWasSaved = true;
                FileSavedAs = filename;
                PathToFile = path;

                ExitWindow();
            }
            catch
            {
                new Alert("You do not have access", this, "Error");
            }


        }
    }
}
