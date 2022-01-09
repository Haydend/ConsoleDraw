using ConsoleDraw.Inputs;
using ConsoleDraw.Windows;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestApp.Windows
{
    public class MainWindow : FullWindow
    {
        public MainWindow() : base(null, 0, 0, Console.WindowWidth, Console.WindowHeight)
        {
            Button oneBtn = new(2, 2, "Button One", "oneBtn", this) { Action = delegate () { _ = new Alert("You Clicked Button One", this, ConsoleColor.White); } };
            Button twoBtn = new(4, 2, "Button Two", "twoBtn", this) { Action = delegate () { _ = new Alert("You Clicked Button Two", this, ConsoleColor.White); } };
            Button threeBtn = new(6, 2, "Long Alert", "threeoBtn", this) { Action = delegate () { _ = new Alert("A web browser (commonly referred to as a browser) is a software application for retrieving, presenting and traversing information resources on the World Wide", this, ConsoleColor.White); } };

            Button displayAlertBtn = new(2, 20, "Display Alert", "displayAlertBtn", this) { Action = delegate () { _ = new Alert("This is an Alert!", this, ConsoleColor.White); } };
            Button displayConfirmBtn = new(4, 20, "Display Confirm", "displayConfirmBtn", this)
            {
                Action = delegate ()
                {
                    Confirm cf = new("This is a Confirm!", this, ConsoleColor.White);

                    if (cf.ShowDialog() == ConsoleDraw.DialogResult.OK)
                    {

                    }
                }
            };
            Button exitBtn = new(6, 20, "Exit", "exitBtn", this) { Action = delegate () { ExitWindow(); } };

            Button displaySettingBtn = new(2, 40, "Display Settings", "displaySettingsBtn", this) { Action = delegate () { _ = new SettingsWindow(this); } };
            Button displaySaveBtn = new(4, 40, "Display Save Menu", "displaySaveMenuBtn", this) { Action = delegate () { _ = new SaveMenu(this, "Untitled.txt", Directory.GetCurrentDirectory(), "Test Data"); } };
            Button displayLoadBtn = new(6, 40, "Display Load Menu", "displayLoadMenuBtn", this) { Action = delegate () { _ = new LoadMenu(this, Directory.GetCurrentDirectory(), new Dictionary<string, string>() { { "txt", "Text Document" }, { "*", "All Files" } }); } };

            CheckBox oneCheckBox = new(10, 2, "oneCheckBox", this);
            Label oneCheckBoxLabel = new("Check Box One", 10, 6, "oneCheckBoxLabel", this);
            CheckBox twoCheckBox = new(12, 2, "twoCheckBox", this) { Checked = true };
            Label twoCheckBoxLabel = new("Check Box Two", 12, 6, "twoCheckBoxLabel", this);
            CheckBox threeCheckBox = new(14, 2, "threeCheckBox", this);
            Label threeCheckBoxLabel = new("Check Box Three", 14, 6, "threeCheckBoxLabel", this);

            Label groupOneLabel = new("Radio Button Group One", 9, 25, "oneCheckBoxLabel", this);
            RadioButton oneRadioBtnGroupOne = new(10, 25, "oneRadioBtnGroupOne", "groupOne", this) { Checked = true };
            Label oneRadioBtnGroupOneLabel = new("Radio Button One", 10, 29, "oneCheckBoxLabel", this);
            RadioButton twoRadioBtnGroupOne = new(12, 25, "twoRadioBtnGroupOne", "groupOne", this);
            Label twoRadioBtnGroupOneLabel = new("Radio Button Two", 12, 29, "oneCheckBoxLabel", this);
            RadioButton threeRadioBtnGroupOne = new(14, 25, "threeRadioBtnGroupOne", "groupOne", this);
            Label threeRadioBtnGroupOneLabel = new("Radio Button Three", 14, 29, "oneCheckBoxLabel", this);

            Label groupTwoLabel = new("Radio Button Group Two", 9, 50, "oneCheckBoxLabel", this);
            RadioButton oneRadioBtnGroupTwo = new(10, 50, "oneRadioBtnGroupTwo", "groupTwo", this) { Checked = true };
            RadioButton twoRadioBtnGroupTwo = new(12, 50, "twoRadioBtnGroupTwo", "groupTwo", this);
            RadioButton threeRadioBtnGroupTwo = new(14, 50, "threeRadioBtnGroupTwo", "groupTwo", this);

            Label textAreaLabel = new("Text Area", 16, 2, "textAreaLabel", this);
            TextArea textArea = new(17, 2, 60, 6, "txtArea", this)
            {
                BackgroundColour = ConsoleColor.DarkGray
            };

            Label txtBoxLabel = new("Text Box", 24, 2, "txtBoxLabel", this);
            TextBox txtBox = new(24, 11, "txtBox", this);

            FileBrowser fileSelect = new(26, 2, 40, 10, Directory.GetCurrentDirectory(), "fileSelect", this, true);

            ProgressBar progressBar = new(10, 39, 2, 3, 70, "progressBar", this);
            Label progressBarLabel = new("10%", 39, 73, "oneCheckBoxLabel", this);

            Button progressBarDownBtn = new(37, 2, "Progress Down", "displaySettingsBtn", this) { Action = delegate () { progressBar.PercentageComplete--; progressBarLabel.SetText(string.Format("{0}%", progressBar.PercentageComplete).PadRight(4)); } };
            Button progressBarUpBtn = new(37, 18, "Progress Up", "displaySettingsBtn", this) { Action = delegate () { progressBar.PercentageComplete++; progressBarLabel.SetText(string.Format("{0}%", progressBar.PercentageComplete).PadRight(4)); } };



            Inputs.Add(oneBtn);
            Inputs.Add(twoBtn);
            Inputs.Add(threeBtn);
            Inputs.Add(oneCheckBox);
            Inputs.Add(oneCheckBoxLabel);
            Inputs.Add(twoCheckBox);
            Inputs.Add(twoCheckBoxLabel);
            Inputs.Add(threeCheckBox);
            Inputs.Add(threeCheckBoxLabel);

            Inputs.Add(displayAlertBtn);
            Inputs.Add(displayConfirmBtn);
            Inputs.Add(exitBtn);

            Inputs.Add(groupOneLabel);
            Inputs.Add(oneRadioBtnGroupOne);
            Inputs.Add(oneRadioBtnGroupOneLabel);
            Inputs.Add(twoRadioBtnGroupOne);
            Inputs.Add(twoRadioBtnGroupOneLabel);
            Inputs.Add(threeRadioBtnGroupOne);
            Inputs.Add(threeRadioBtnGroupOneLabel);

            Inputs.Add(displaySettingBtn);
            Inputs.Add(displaySaveBtn);
            Inputs.Add(displayLoadBtn);

            Inputs.Add(groupTwoLabel);
            Inputs.Add(oneRadioBtnGroupTwo);
            Inputs.Add(twoRadioBtnGroupTwo);
            Inputs.Add(threeRadioBtnGroupTwo);

            Inputs.Add(textAreaLabel);
            Inputs.Add(textArea);

            Inputs.Add(txtBoxLabel);
            Inputs.Add(txtBox);

            Inputs.Add(fileSelect);

            Inputs.Add(progressBarDownBtn);
            Inputs.Add(progressBarUpBtn);

            Inputs.Add(progressBar);
            Inputs.Add(progressBarLabel);

            List<string> opts = new() { "hello", "world" };
            Dropdown cb = new(0, 0, opts, "cb", this)
            {
                DropdownItems = new List<DropdownItem>(opts.Select(_ => new DropdownItem(_, 10, "2", this)).ToArray())
            };

            Inputs.Add(cb);

            CurrentlySelected = oneBtn;

            Draw();
            MainLoop();
        }

    }
}