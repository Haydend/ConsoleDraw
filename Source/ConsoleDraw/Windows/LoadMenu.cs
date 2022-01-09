using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleDraw.Windows
{
    public class LoadMenu : PopupWindow
    {
        private Button loadBtn;
        private Button cancelBtn;
        private TextBox openTxtBox;
        private FileBrowser fileSelect;
        private Dropdown fileTypeDropdown;

        public Boolean DataLoaded;
        public String Data;
        public String FileNameLoaded;
        public String PathOfLoaded;
        public Dictionary<String, String> FileTypes;

        public LoadMenu(String path, Dictionary<String, String> fileTypes, Window parentWindow)
            : base("Load Menu", Math.Min(6, Console.WindowHeight - 22), (Console.WindowWidth / 2) - 30, 60, 20, parentWindow)
        {
            BackgroundColour = ConsoleColor.White;
            FileTypes = fileTypes;

            fileSelect = new FileBrowser(PostionX + 2, PostionY + 2, 56, 13, path, "fileSelect", this, true, "txt")
            {
                ChangeItem = delegate () { UpdateCurrentlySelectedFileName(); },
                SelectFile = delegate () { LoadFile(); }
            };

            Label openLabel = new Label("Open", PostionX + 16, PostionY + 2, "openLabel", this);
            openTxtBox = new TextBox(PostionX + 16, PostionY + 7, "openTxtBox", this, Width - 13) { Selectable = false };

            fileTypeDropdown = new Dropdown(PostionX + 18, PostionY + 40, FileTypes.Select(x => x.Value).ToList(), "fileTypeDropdown", this, 17)
            {
                OnUnselect = delegate () { UpdateFileTypeFilter(); }
            };

            loadBtn = new Button(PostionX + 18, PostionY + 2, "Load", "loadBtn", this)
            {
                Action = delegate () { LoadFile(); }
            };
            cancelBtn = new Button(PostionX + 18, PostionY + 9, "Cancel", "cancelBtn", this)
            {
                Action = delegate () { ExitWindow(); }
            };

            Inputs.Add(fileSelect);
            Inputs.Add(loadBtn);
            Inputs.Add(cancelBtn);
            Inputs.Add(openLabel);
            Inputs.Add(openTxtBox);
            Inputs.Add(fileTypeDropdown);

            CurrentlySelected = fileSelect;

            Draw();
            MainLoop();
        }

        private void UpdateCurrentlySelectedFileName()
        {
            string CurrentlySelectedFile = fileSelect.CurrentlySelectedFile;
            openTxtBox.SetText(CurrentlySelectedFile);
        }

        private void UpdateFileTypeFilter()
        {
            KeyValuePair<string, string> filter = FileTypes.FirstOrDefault(x => x.Value == fileTypeDropdown.Text);
            KeyValuePair<string, string> currentFilter = FileTypes.FirstOrDefault(x => x.Key == fileSelect.FilterByExtension);

            if (currentFilter.Key != filter.Key)
            {
                fileSelect.FilterByExtension = filter.Key;
                fileSelect.GetFileNames();
                fileSelect.Draw();
            }
        }

        private void LoadFile()
        {
            if (fileSelect.CurrentlySelectedFile == "")
            {
                new Alert("No file Selected", this, "Warning");
                return;
            }

            string file = Path.Combine(fileSelect.CurrentPath, fileSelect.CurrentlySelectedFile);
            String text = System.IO.File.ReadAllText(file);

            /*MainWindow mainWindow = (MainWindow)ParentWindow;
            mainWindow.textArea.SetText(text);
            mainWindow.fileLabel.SetText(fileSelect.CurrentlySelectedFile);*/

            DataLoaded = true;
            Data = text;
            FileNameLoaded = fileSelect.CurrentlySelectedFile;
            PathOfLoaded = fileSelect.CurrentPath;

            ExitWindow();
        }
    }
}
