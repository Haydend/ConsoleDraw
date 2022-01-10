using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;

namespace TestApp.Windows
{
    public class SettingsWindow : PopupWindow
    {
        public SettingsWindow(Window parentWindow)
            : base(parentWindow, "Settings", 6, 10, 80, 20)
        {
            BackgroundColour = ConsoleColor.White;

            Label appTitleLabel = new(this, "App Title", 8, 12, "appTitleLabel");
            TextBox appTitleTxtBox = new(this, 8, 25, Console.Title, "appTitleTxtBox", 10);

            Label saveOnExitLabel = new(this, "Save On Exit", 10, 12, "saveOnExitLabel");
            CheckBox saveOneExitChkBox = new(this, 10, 25, "saveOnExitCheckBox");

            Label byAllLabel = new(this, "For All", 12, 12, "forAll");
            RadioButton byAllRadioBtn = new(this, 12, 25, "byAllRadioBtn", "Users") { Checked = true };
            Label justYouLabel = new(this, "Just You", 14, 12, "justYou");
            RadioButton justYouRadioBtn = new(this, 14, 25, "justYouRadioBtn", "Users");

            Button applyBtn = new(this, 24, 12, "Apply", "exitBtn") { Action = delegate () { ExitWindow(); } };
            Button exitBtn = new(this, 24, 20, "Exit", "exitBtn") { Action = delegate () { ExitWindow(); } };

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
