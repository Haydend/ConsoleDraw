using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleDraw.Windows.Base;
using ConsoleDraw.Inputs;
using ConsoleDraw.Windows;
using System.IO;

namespace TestApp.Windows
{
    public class MainWindow : FullWindow
    {
        public MainWindow() : base(0, 0, Console.WindowWidth, Console.WindowHeight, null)
        {
            var oneBtn = new Button(2, 2, "Button One", "oneBtn", this) { Action = delegate() { new Alert("You Clicked Button One", this, ConsoleColor.White); } };
            var twoBtn = new Button(4, 2, "Button Two", "twoBtn", this) { Action = delegate() { new Alert("You Clicked Button Two", this, ConsoleColor.White); } };
            var threeBtn = new Button(6, 2, "Long Alert", "threeoBtn", this) { Action = delegate() { new Alert("A web browser (commonly referred to as a browser) is a software application for retrieving, presenting and traversing information resources on the World Wide", this, ConsoleColor.White); } };

            var displayAlertBtn = new Button(2, 20, "Display Alert", "displayAlertBtn", this) { Action = delegate() { new Alert("This is an Alert!", this, ConsoleColor.White); } };
            var displayConfirmBtn = new Button(4, 20, "Display Confirm", "displayConfirmBtn", this) { Action = delegate() {
                var cf = new Confirm("This is a Confirm!", this, ConsoleColor.White);
                
                if(cf.ShowDialog() == ConsoleDraw.DialogResult.OK)
                {

                }
            } };
            var exitBtn = new Button(6, 20, "Exit", "exitBtn", this) { Action = delegate() { ExitWindow(); } };

            var displaySettingBtn = new Button(2, 40, "Display Settings", "displaySettingsBtn", this) { Action = delegate() { new SettingsWindow(this); } };
            var displaySaveBtn = new Button(4, 40, "Display Save Menu", "displaySaveMenuBtn", this) { Action = delegate() { new SaveMenu("Untitled.txt", Directory.GetCurrentDirectory(), "Test Data", this); } };
            var displayLoadBtn = new Button(6, 40, "Display Load Menu", "displayLoadMenuBtn", this) { Action = delegate() { new LoadMenu(Directory.GetCurrentDirectory(), new Dictionary<string, string>() {{"txt", "Text Document"}, {"*","All Files"}}, this); } };

            var oneCheckBox = new CheckBox(10, 2, "oneCheckBox", this);
            var oneCheckBoxLabel = new Label("Check Box One", 10, 6, "oneCheckBoxLabel", this);
            var twoCheckBox = new CheckBox(12, 2, "twoCheckBox", this) { Checked = true };
            var twoCheckBoxLabel = new Label("Check Box Two", 12, 6, "twoCheckBoxLabel", this);
            var threeCheckBox = new CheckBox(14, 2, "threeCheckBox", this);
            var threeCheckBoxLabel = new Label("Check Box Three", 14, 6, "threeCheckBoxLabel", this);

            var groupOneLabel = new Label("Radio Button Group One", 9, 25, "oneCheckBoxLabel", this);
            var oneRadioBtnGroupOne = new RadioButton(10, 25, "oneRadioBtnGroupOne", "groupOne", this) { Checked = true };
            var oneRadioBtnGroupOneLabel = new Label("Radio Button One", 10, 29, "oneCheckBoxLabel", this);
            var twoRadioBtnGroupOne = new RadioButton(12, 25, "twoRadioBtnGroupOne", "groupOne", this);
            var twoRadioBtnGroupOneLabel = new Label("Radio Button Two", 12, 29, "oneCheckBoxLabel", this);
            var threeRadioBtnGroupOne = new RadioButton(14, 25, "threeRadioBtnGroupOne", "groupOne", this);
            var threeRadioBtnGroupOneLabel = new Label("Radio Button Three", 14, 29, "oneCheckBoxLabel", this);

            var groupTwoLabel = new Label("Radio Button Group Two", 9, 50, "oneCheckBoxLabel", this);
            var oneRadioBtnGroupTwo = new RadioButton(10, 50, "oneRadioBtnGroupTwo", "groupTwo", this) { Checked = true };
            var twoRadioBtnGroupTwo = new RadioButton(12, 50, "twoRadioBtnGroupTwo", "groupTwo", this);
            var threeRadioBtnGroupTwo = new RadioButton(14, 50, "threeRadioBtnGroupTwo", "groupTwo", this);

            var textAreaLabel = new Label("Text Area", 16, 2, "textAreaLabel", this);
            var textArea = new TextArea(17, 2, 60, 6, "txtArea", this);
            textArea.BackgroundColour = ConsoleColor.DarkGray;

            var txtBoxLabel = new Label("Text Box", 24, 2, "txtBoxLabel", this);
            var txtBox = new TextBox(24, 11, "txtBox", this);

            var fileSelect = new FileBrowser(26, 2, 40, 10, Directory.GetCurrentDirectory(), "fileSelect", this, true);

            var progressBar = new ProgressBar(10, 39, 2, 3, 70, "progressBar", this);
            var progressBarLabel = new Label("10%", 39, 73, "oneCheckBoxLabel", this);

            var progressBarDownBtn = new Button(37, 2, "Progress Down", "displaySettingsBtn", this) { Action = delegate () { progressBar.PercentageComplete--; progressBarLabel.SetText(String.Format("{0}%", progressBar.PercentageComplete).PadRight(4)); } };
            var progressBarUpBtn = new Button(37, 18, "Progress Up", "displaySettingsBtn", this) { Action = delegate () { progressBar.PercentageComplete++; progressBarLabel.SetText(String.Format("{0}%", progressBar.PercentageComplete).PadRight(4)); } };

           

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

            List<string> opts = new List<string>() { "hello", "world"};
            var cb = new Dropdown(0, 0, opts, "cb", this);
            cb.DropdownItems = new List<DropdownItem>(opts.Select(_=>new DropdownItem(_,10,"2", this)).ToArray());

            Inputs.Add(cb);

            CurrentlySelected = oneBtn;

            Draw();
            MainLoop();
        }

    }
}