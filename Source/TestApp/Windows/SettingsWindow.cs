using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Windows
{
    public class SettingsWindow : PopupWindow
    {
        public SettingsWindow(Window parentWindow)
            : base("Settings", 6, 10, 80, 20, parentWindow)
        {
            BackgroundColour = ConsoleColor.White;

            var appTitleLabel = new Label("App Title", 8, 12, "appTitleLabel", this);
            var appTitleTxtBox = new TextBox(8, 25, Console.Title, "appTitleTxtBox", this, 10);

            var saveOnExitLabel = new Label("Save On Exit", 10, 12, "saveOnExitLabel", this);
            var saveOneExitChkBox = new CheckBox(10, 25, "saveOnExitCheckBox", this);

            var byAllLabel = new Label("For All", 12, 12, "forAll", this);
            var byAllRadioBtn = new RadioButton(12, 25, "byAllRadioBtn", "Users", this) { Checked = true };
            var justYouLabel = new Label("Just You", 14, 12, "justYou", this);
            var justYouRadioBtn = new RadioButton(14, 25, "justYouRadioBtn", "Users", this);

            var applyBtn = new Button(24, 12, "Apply", "exitBtn", this) { Action = delegate() { ExitWindow(); } };
            var exitBtn = new Button(24, 20, "Exit", "exitBtn", this) { Action = delegate() { ExitWindow(); } };

            Inputs.Add(appTitleLabel);
            Inputs.Add(appTitleTxtBox);

            Inputs.Add(saveOnExitLabel);
            Inputs.Add(saveOneExitChkBox);

            Inputs.Add(byAllLabel);
            Inputs.Add(byAllRadioBtn);
            Inputs.Add(justYouLabel);
            Inputs.Add(justYouRadioBtn);

            Inputs.Add(applyBtn);
            Inputs.Add(exitBtn);
            
            
            CurrentlySelected = exitBtn;

            Draw();
            MainLoop();
        }


    }
}
