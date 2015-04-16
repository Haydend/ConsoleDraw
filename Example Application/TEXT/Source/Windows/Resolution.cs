using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDraw.Windows.Base;
using ConsoleDraw.Inputs;
using ConsoleDraw.Windows;
using ConsoleDraw;

namespace Text
{
    public class Resolution : PopupWindow
    {
        private Button applyBtn;
        private Button exitBtn;
        private TextBox widthTxtBox;
        private TextBox heightTxtBox;

        public Resolution(Window parentWindow)
            : base("Change Resolution", 10, 45, 30, 8, parentWindow)
        {
            var widthLabel = new Label("Width", 12, 48, "widthLabel", parentWindow);
            widthTxtBox = new TextBox(12, 56, Console.WindowWidth.ToString(), "widthTxtBox", this, 5);
            var widthMaxBtn = new Button(12, 64, "Max", "widthMaxBtn", this);
            widthMaxBtn.Action = delegate() { widthTxtBox.SetText(Console.LargestWindowWidth.ToString()); };

            var heightLabel = new Label("Height", 14, 48, "widthLabel", parentWindow);
            heightTxtBox = new TextBox(14, 56, Console.WindowHeight.ToString(), "heightTxtBox", this, 5);
            var heightMaxBtn = new Button(14, 64, "Max", "heighthMaxBtn", this);
            heightMaxBtn.Action = delegate() { heightTxtBox.SetText(Console.LargestWindowHeight.ToString()); };

            applyBtn = new Button(16, 48, "Apply", "applyBtn", this);
            applyBtn.Action = delegate() { Apply(); };
            
            exitBtn = new Button(16, 57, "Exit", "exitBtn", this);
            exitBtn.Action = delegate() { ExitWindow(); };

            Inputs.Add(widthLabel);
            Inputs.Add(widthTxtBox);
            Inputs.Add(widthMaxBtn);
            Inputs.Add(heightLabel);
            Inputs.Add(heightTxtBox);
            Inputs.Add(heightMaxBtn);
            Inputs.Add(applyBtn);
            Inputs.Add(exitBtn);

            CurrentlySelected = applyBtn;

            Draw();
            MainLoop();
        }

        public void Apply()
        {
            Int32 newWidth = 0;
            if (!Int32.TryParse(widthTxtBox.GetText(),out newWidth))
            {
                new Alert(this, "Width must be a number", "Error");
                return;
            }

            Int32 newHeight = 0;
            if (!Int32.TryParse(heightTxtBox.GetText(), out newHeight))
            {
                new Alert(this, "Height must be a number", "Error");
                return;
            }

            WindowManager.UpdateWindow(newWidth, newHeight);

            
            Draw();
        }
    }
}
