using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDraw.Windows.Base;
using ConsoleDraw.Inputs;
using ConsoleDraw.Windows;

namespace Text
{
    public class LoadMenu : PopupWindow
    {
        private Button loadBtn;
        private Button cancelBtn;
        private TextBox openTxtBox;
        private FileSelect fileSelect;
        private Dropdown fileTypeDropdown;

        public Boolean DataLoaded;
        public String Data;
        public String FileNameLoaded;
        public String PathOfLoaded;
        public Dictionary<String, String> FileTypes;

        public LoadMenu(Dictionary<String, String> fileTypes, Window parentWindow)
            : base("Load Menu", 10, 45, 60, 20, parentWindow)
        {
            FileTypes = fileTypes;
            
            fileSelect = new FileSelect(12, 47, FileInfo.Path, "fileSelect", this, true, "txt");
            fileSelect.ChangeItem = delegate() { UpdateCurrentlySelectedFileName(); };
            fileSelect.SelectFile = delegate() { LoadFile(); };

            var openLabel = new Label("Open", 26, 47, "openLabel", this);
            openTxtBox = new TextBox(26, 53, "openTxtBox", this, 31) { Selectable = false };

            fileTypeDropdown = new Dropdown(28, 86, FileTypes.Select(x => x.Value).ToList(), "fileTypeDropdown", this, 17);
            fileTypeDropdown.OnUnselect = delegate() { UpdateFileTypeFilter(); };

            loadBtn = new Button(28, 48, "Load", "loadBtn", this);
            loadBtn.Action = delegate() { LoadFile(); };
            cancelBtn = new Button(28, 56, "Cancel", "cancelBtn", this);
            cancelBtn.Action = delegate() { ExitWindow(); };

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
            var CurrentlySelectedFile = fileSelect.CurrentlySelectedFile;
            openTxtBox.SetText(CurrentlySelectedFile);
        }

        private void UpdateFileTypeFilter()
        {
            var filter = FileTypes.FirstOrDefault(x => x.Value == fileTypeDropdown.Text);
            var currentFilter = FileTypes.FirstOrDefault(x => x.Key == fileSelect.FilterByExtension);

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

            var file = Path.Combine(fileSelect.CurrentPath, fileSelect.CurrentlySelectedFile);
            String text = System.IO.File.ReadAllText(file);

            /*var mainWindow = (MainWindow)ParentWindow;
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
