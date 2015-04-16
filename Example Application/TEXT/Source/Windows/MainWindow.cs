using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDraw.Windows.Base;
using ConsoleDraw.Inputs;
using ConsoleDraw;
using ConsoleDraw.Windows;

namespace Text
{
    public class MainWindow : FullWindow
    {
        private Menu fileMenu;
        private Menu settingMenu;
        private Menu helpMenu;
        public TextArea textArea;
        public Label fileLabel;

        public MainWindow()
            : base(0, 0, Console.WindowWidth, Console.WindowHeight, null)
        {
            fileLabel = new Label(' ' + FileInfo.Filename + ' ', 2, 60, "fileLabel", this);

            fileMenu = BulidFileMenu();
            settingMenu = BuildSettingMenu(); 
            helpMenu = BulidHelpMenu();

            textArea = new TextArea(3, 1, Console.WindowWidth - 3, Console.WindowHeight - 5, "textArea", this);
            textArea.OnChange = delegate() { FileInfo.HasChanged = true; };

            Inputs.Add(fileMenu);
            Inputs.Add(settingMenu);
            Inputs.Add(helpMenu);

            Inputs.Add(fileLabel);
            Inputs.Add(textArea);
            

            CurrentlySelected = textArea;
            Draw();
            MainLoop();
        }

        public override void ReDraw()
        {
            textArea.Height = Console.WindowHeight - 5;
            textArea.Width = Console.WindowWidth - 3;
            
            //Black Boarder
            WindowManager.DrawColourBlock(ConsoleColor.Black, 2, 1, 3, Console.WindowWidth - 2); //Top
        }

        private Menu BulidFileMenu()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            Menu fileMenu = new Menu("File", 1, 2, "fileMenu", this);

            var newMenuItem = new MenuItem("New", "fileMenuMenuItemNew", fileMenu.MenuDropdown);
            newMenuItem.Action = delegate() { NewFile(newMenuItem.ParentWindow); };
            menuItems.Add(newMenuItem);

            var loadMenuItem = new MenuItem("Load", "fileMenuMenuItemLoad", fileMenu.MenuDropdown);
            loadMenuItem.Action = delegate() { LoadData(loadMenuItem.ParentWindow); };
            menuItems.Add(loadMenuItem);

            var saveMenuItem = new MenuItem("Save", "fileMenuMenuItemSave", fileMenu.MenuDropdown);
            saveMenuItem.Action = delegate() { SaveData(saveMenuItem.ParentWindow); };
            menuItems.Add(saveMenuItem);

            var exitMenuItem = new MenuItem("Exit", "fileMenuMenuItemExit", fileMenu.MenuDropdown);
            exitMenuItem.Action = delegate() { ExitApp(saveMenuItem.ParentWindow); };
            menuItems.Add(exitMenuItem);

            fileMenu.MenuItems.AddRange(menuItems);

            return fileMenu;
        }

        private Menu BuildSettingMenu()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            Menu settingMenu = new Menu("Settings", 1, 10, "settingMenu", this);

            var resolutionMenuItem = new MenuItem("Resolution", "settingMenuMenuItemResolution", fileMenu.MenuDropdown);
            resolutionMenuItem.Action = delegate() { new Resolution(resolutionMenuItem.ParentWindow); };
            menuItems.Add(resolutionMenuItem);

            var tempMenuItem = new MenuItem("Temp", "settingMenuMenuItemTemp", fileMenu.MenuDropdown);
            tempMenuItem.Action = delegate() { new Alert(tempMenuItem.ParentWindow, "Coming Soon!"); };
            menuItems.Add(tempMenuItem);

            settingMenu.MenuItems.AddRange(menuItems);

            return settingMenu;
        }

        private Menu BulidHelpMenu()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            Menu helpMenu = new Menu("Help", 1, 22, "helpMenu", this);

            var viewHelpMenuItem = new MenuItem("Help", "fileMenuMenuItemViewHelp", fileMenu.MenuDropdown);
            viewHelpMenuItem.Action = delegate() { new Alert(viewHelpMenuItem.ParentWindow, "Coming Soon!"); };
            menuItems.Add(viewHelpMenuItem);

            var aboutMenuItem = new MenuItem("About", "fileMenuMenuItemAbout", fileMenu.MenuDropdown);
            aboutMenuItem.Action = delegate() { new Alert(viewHelpMenuItem.ParentWindow, "Does anyone ever read this?"); };
            menuItems.Add(aboutMenuItem);

            helpMenu.MenuItems.AddRange(menuItems);

            return helpMenu;
        }

        private void ExitApp(Window parent)
        {
            if (FileInfo.HasChanged)
            {
                var saveCheck = new Confirm(parent, "You have unsaved changes! Do you wish to save?", "Save");
                if (saveCheck.Result) //User wishs to save
                {
                    if (!SaveData(parent)) //Save was cancelled
                        return;
                }
            }
            
            var exitCheck = new Confirm(parent, "Are you sure you wish to Exit?", "Exit");
            if (!exitCheck.Result)
                return;

            ExitWindow();
        }

        public void LoadData(Window parent)
        {
            var loadMenu = new LoadMenu(parent);

            if (loadMenu.DataLoaded) //Data Load was successful
            {
                parent.ExitWindow();
                
                textArea.SetText(loadMenu.Data);
                fileLabel.SetText(' ' + loadMenu.FileNameLoaded + ' ');

                Draw();

                FileInfo.Filename = loadMenu.FileNameLoaded;
                FileInfo.Path = loadMenu.PathOfLoaded;
                FileInfo.HasChanged = false;
            }
        }

        public Boolean SaveData(Window parent)
        {
            var saveMenu = new SaveMenu(textArea.GetText(), parent);

            if (saveMenu.FileWasSaved) //Data Save was successful
            {
                parent.ExitWindow();

                fileLabel.SetText(' ' + saveMenu.FileSavedAs + ' ');

                Draw();

                FileInfo.Filename = saveMenu.FileSavedAs;
                FileInfo.Path = saveMenu.PathToFile;
                FileInfo.HasChanged = false;
            }

            return saveMenu.FileWasSaved;
        }

        public void NewFile(Window parent)
        {
            if (FileInfo.HasChanged)
            {
                var saveCheck = new Confirm(parent, "You have unsaved changes! Do you wish to save?", "Save");
                if (saveCheck.Result) //User wishs to save
                {
                    if (!SaveData(parent)) //Save was cancelled
                        return;
                }
            }

            FileInfo.Filename = "Untitled.txt";
            FileInfo.HasChanged = false;

            textArea.SetText("");
            fileLabel.SetText(FileInfo.Filename);

            parent.ExitWindow();
            SelectItemByID("textArea");

            Draw();
        }
    }

    
}
