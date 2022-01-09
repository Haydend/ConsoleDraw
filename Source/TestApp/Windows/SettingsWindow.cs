using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;

namespace TestApp.Windows
{
    public class SettingsWindow : PopupWindow
    {
        public SettingsWindow(Window parentWindow)
            : base("Settings", 6, 10, 80, 20, parentWindow)
        {
            BackgroundColour = ConsoleColor.White;

            Label appTitleLabel = new("App Title", 8, 12, "appTitleLabel", this);
            TextBox appTitleTxtBox = new(8, 25, Console.Title, "appTitleTxtBox", this, 10);

            Label saveOnExitLabel = new("Save On Exit", 10, 12, "saveOnExitLabel", this);
            CheckBox saveOneExitChkBox = new(10, 25, "saveOnExitCheckBox", this);

            Label byAllLabel = new("For All", 12, 12, "forAll", this);
            RadioButton byAllRadioBtn = new(12, 25, "byAllRadioBtn", "Users", this) { Checked = true };
            Label justYouLabel = new("Just You", 14, 12, "justYou", this);
            RadioButton justYouRadioBtn = new(14, 25, "justYouRadioBtn", "Users", this);

            Button applyBtn = new(24, 12, "Apply", "exitBtn", this) { Action = delegate () { ExitWindow(); } };
            Button exitBtn = new(24, 20, "Exit", "exitBtn", this) { Action = delegate () { ExitWindow(); } };

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
