using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC
{
    public class MainWindow : PopupWindow
    {
        public bool changePath = false;
        public String Path = Directory.GetCurrentDirectory();

        public MainWindow()
            : base("Change Folder", 1, (Console.WindowWidth / 2) - ((Console.WindowWidth - 4) / 2), Console.WindowWidth - 4, Console.WindowHeight - 2, null)
        {
            var folderSelect = new FileSelect(3, 3, Console.WindowWidth - 6, Height - 6, Directory.GetCurrentDirectory(), "folderSelect", this, true);
            folderSelect.SelectFile = delegate() { Path = folderSelect.CurrentPath; changePath = true; ExitWindow(); };

            var selectBtn = new Button(PostionX + Height - 2, 3, "Select", "selectBtn", this);
            selectBtn.Action = delegate() { Path = folderSelect.CurrentPath; changePath = true; ExitWindow(); };

            var cancelBtn = new Button(PostionX + Height - 2, 12, "Cancel", "cancelBtn", this);
            cancelBtn.Action = delegate() { changePath = false; ExitWindow(); };

            Inputs.Add(folderSelect);
            Inputs.Add(selectBtn);
            Inputs.Add(cancelBtn);

            CurrentlySelected = selectBtn;

            Draw();
            MainLoop();
        }

    }
}
