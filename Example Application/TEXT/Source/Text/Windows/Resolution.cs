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
            : base("Change Resolution", 6, (Console.WindowWidth / 2) - 15, 30, 8, parentWindow)
        {
            var widthLabel = new Label("Width", PostionX + 2, PostionY + 2, "widthLabel", parentWindow) { BackgroundColour = ConsoleColor.Gray };
            widthTxtBox = new TextBox(PostionX + 2, PostionY + 10, Console.WindowWidth.ToString(), "widthTxtBox", this, 5);
            var widthMaxBtn = new Button(PostionX + 2, PostionY + 17, "Max", "widthMaxBtn", this);
            widthMaxBtn.Action = delegate() { widthTxtBox.SetText(Console.LargestWindowWidth.ToString()); };

            var heightLabel = new Label("Height", PostionX + 4, PostionY + 2, "widthLabel", parentWindow) { BackgroundColour = ConsoleColor.Gray };
            heightTxtBox = new TextBox(PostionX + 4, PostionY + 10, Console.WindowHeight.ToString(), "heightTxtBox", this, 5);
            var heightMaxBtn = new Button(PostionX + 4, PostionY + 17, "Max", "heighthMaxBtn", this);
            heightMaxBtn.Action = delegate() { heightTxtBox.SetText(Console.LargestWindowHeight.ToString()); };

            applyBtn = new Button(PostionX + 6, PostionY + 2, "Apply", "applyBtn", this);
            applyBtn.Action = delegate() { Apply(); };

            exitBtn = new Button(PostionX + 6, PostionY + 10, "Exit", "exitBtn", this);
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
                new Alert("Width must be a number", this, "Error");
                return;
            }

            Int32 newHeight = 0;
            if (!Int32.TryParse(heightTxtBox.GetText(), out newHeight))
            {
                new Alert("Height must be a number", this, "Error");
                return;
            }

            try
            {
                WindowManager.UpdateWindow(newWidth, newHeight);
            }
            catch (ArgumentOutOfRangeException e)
            {
                new Alert("Window can not be that size", this);
            }



            ExitWindow();
        }
    }
}
