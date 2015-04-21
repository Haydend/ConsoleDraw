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
        private FileBrowser fileSelect;
        private String Text;

        public Boolean FileWasSaved;
        public String FileSavedAs;
        public String PathToFile;

        public SaveMenu(String data, Window parentWindow)
            : base("Save Menu", 6, (Console.WindowWidth / 2) - 30, 60, 20, parentWindow)
        {
            Text = data;

            fileSelect = new FileBrowser(PostionX + 2, PostionY + 2, Width - 4, 13, FileInfo.Path, "fileSelect", this);

            var openLabel = new Label("Name", PostionX + 16, PostionY + 2, "openLabel", this);
            openTxtBox = new TextBox(PostionX + 16, PostionY + 7, FileInfo.Filename, "openTxtBox", this) { Selectable = true };

            saveBtn = new Button(PostionX + 18, PostionY + 2, "Save", "loadBtn", this);
            saveBtn.Action = delegate() { SaveFile(); };
            cancelBtn = new Button(PostionX + 18, PostionY + 9, "Cancel", "cancelBtn", this);
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
                new Alert("You do not have access", this, "Error");
            }

            
        }
    }
}
