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
    public class SaveMenu : PopupWindow
    {
        private Button saveBtn;
        private Button cancelBtn;
        private TextBox openTxtBox;
        private FileSelect fileSelect;
        private String Text;

        public Boolean FileWasSaved;
        public String FileSavedAs;
        public String PathToFile;

        public SaveMenu(String data, Window parentWindow)
            : base("Save Menu", 10, 45, 60, 20, parentWindow)
        {
            Text = data;
            
            fileSelect = new FileSelect(12, 47, FileInfo.Path, "fileSelect", this);

            var openLabel = new Label("Name", 26, 47, "openLabel", this);
            openTxtBox = new TextBox(26, 53, FileInfo.Filename, "openTxtBox", this) { Selectable = true };

            saveBtn = new Button(28, 48, "Save", "loadBtn", this);
            saveBtn.Action = delegate() { SaveFile(); };
            cancelBtn = new Button(28, 56, "Cancel", "cancelBtn", this);
            cancelBtn.Action = delegate() { ExitWindow(); };

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
            var path = fileSelect.CurrentPath;
            var filename = openTxtBox.GetText();

            var fullFile = Path.Combine(path, filename);

            try
            {
                StreamWriter file = new StreamWriter(fullFile);

                file.Write(Text);

                file.Close();

                FileWasSaved = true;
                FileSavedAs = filename;
                PathToFile = path;

                ExitWindow();
            }
            catch
            { 
                new Alert(this, "You do not have access", "Error");
            }

            
        }
    }
}
