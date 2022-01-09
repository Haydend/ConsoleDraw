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
            Button oneBtn = new(this, 2, 2, "Button One", "oneBtn") { Action = delegate () { _ = new Alert("You Clicked Button One", this, ConsoleColor.White); } };
            Button twoBtn = new(this, 4, 2, "Button Two", "twoBtn") { Action = delegate () { _ = new Alert("You Clicked Button Two", this, ConsoleColor.White); } };
            Button threeBtn = new(this, 6, 2, "Long Alert", "threeoBtn") { Action = delegate () { _ = new Alert("A web browser (commonly referred to as a browser) is a software application for retrieving, presenting and traversing information resources on the World Wide", this, ConsoleColor.White); } };

            Button displayAlertBtn = new(this, 2, 20, "Display Alert", "displayAlertBtn") { Action = delegate () { _ = new Alert("This is an Alert!", this, ConsoleColor.White); } };
            Button displayConfirmBtn = new(this, 4, 20, "Display Confirm", "displayConfirmBtn") { Action = delegate () { Confirm cf = new("This is a Confirm!", this, ConsoleColor.White); if (cf.ShowDialog() == ConsoleDraw.DialogResult.OK) { } } };
            Button exitBtn = new(this, 6, 20, "Exit", "exitBtn") { Action = delegate () { ExitWindow(); } };

            Button displaySettingBtn = new(this, 2, 40, "Display Settings", "displaySettingsBtn") { Action = delegate () { _ = new SettingsWindow(this); } };
            Button displaySaveBtn = new(this, 4, 40, "Display Save Menu", "displaySaveMenuBtn") { Action = delegate () { _ = new SaveMenu(this, "Untitled.txt", Directory.GetCurrentDirectory(), "Test Data"); } };
            Button displayLoadBtn = new(this, 6, 40, "Display Load Menu", "displayLoadMenuBtn") { Action = delegate () { _ = new LoadMenu(this, Directory.GetCurrentDirectory(), new Dictionary<string, string>() { { "txt", "Text Document" }, { "*", "All Files" } }); } };

            CheckBox oneCheckBox = new(this, 10, 2, "oneCheckBox");
            Label oneCheckBoxLabel = new(this, "Check Box One", 10, 6, "oneCheckBoxLabel");
            CheckBox twoCheckBox = new(this, 12, 2, "twoCheckBox") { Checked = true };
            Label twoCheckBoxLabel = new(this, "Check Box Two", 12, 6, "twoCheckBoxLabel");
            CheckBox threeCheckBox = new(this, 14, 2, "threeCheckBox");
            Label threeCheckBoxLabel = new(this, "Check Box Three", 14, 6, "threeCheckBoxLabel");

            Label groupOneLabel = new(this, "Radio Button Group One", 9, 25, "oneCheckBoxLabel");
            RadioButton oneRadioBtnGroupOne = new(this, 10, 25, "oneRadioBtnGroupOne", "groupOne") { Checked = true };
            Label oneRadioBtnGroupOneLabel = new(this, "Radio Button One", 10, 29, "oneCheckBoxLabel");
            RadioButton twoRadioBtnGroupOne = new(this, 12, 25, "twoRadioBtnGroupOne", "groupOne");
            Label twoRadioBtnGroupOneLabel = new(this, "Radio Button Two", 12, 29, "oneCheckBoxLabel");
            RadioButton threeRadioBtnGroupOne = new(this, 14, 25, "threeRadioBtnGroupOne", "groupOne");
            Label threeRadioBtnGroupOneLabel = new(this, "Radio Button Three", 14, 29, "oneCheckBoxLabel");

            Label groupTwoLabel = new(this, "Radio Button Group Two", 9, 50, "oneCheckBoxLabel");
            RadioButton oneRadioBtnGroupTwo = new(this, 10, 50, "oneRadioBtnGroupTwo", "groupTwo") { Checked = true };
            RadioButton twoRadioBtnGroupTwo = new(this, 12, 50, "twoRadioBtnGroupTwo", "groupTwo");
            RadioButton threeRadioBtnGroupTwo = new(this, 14, 50, "threeRadioBtnGroupTwo", "groupTwo");

            Label textAreaLabel = new(this, "Text Area", 16, 2, "textAreaLabel");
            TextArea textArea = new(this, 17, 2, 60, 6, "txtArea")
            {
                BackgroundColour = ConsoleColor.DarkGray
            };

            Label txtBoxLabel = new(this, "Text Box", 24, 2, "txtBoxLabel");
            TextBox txtBox = new(this, 24, 11, "txtBox");

            FileBrowser fileSelect = new(this, 26, 2, 40, 10, Directory.GetCurrentDirectory(), "fileSelect", true);

            ProgressBar progressBar = new(this, 10, 39, 2, 3, 70, "progressBar");
            Label progressBarLabel = new(this, "10%", 39, 73, "oneCheckBoxLabel");

            Button progressBarDownBtn = new(this, 37, 2, "Progress Down", "displaySettingsBtn") { Action = delegate () { progressBar.PercentageComplete--; progressBarLabel.SetText(string.Format("{0}%", progressBar.PercentageComplete).PadRight(4)); } };
            Button progressBarUpBtn = new(this, 37, 18, "Progress Up", "displaySettingsBtn") { Action = delegate () { progressBar.PercentageComplete++; progressBarLabel.SetText(string.Format("{0}%", progressBar.PercentageComplete).PadRight(4)); } };



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
            Dropdown cb = new(this, 0, 0, opts, "cb")
            {
                DropdownItems = new List<DropdownItem>(opts.Select(_ => new DropdownItem(this, _, 10, "2")).ToArray())
            };

            Inputs.Add(cb);

            CurrentlySelected = oneBtn;

            Draw();
            MainLoop();
        }

    }
}