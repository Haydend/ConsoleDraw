using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Windows
{
    public class Confirm : PopupWindow
    {
        private Button okBtn;
        private Button cancelBtn;
        public bool Result { get; private set; }

        public Confirm(Window parentWindow, String Message, String Title = "Confirm")
            : base(Title, 6, (Console.WindowWidth / 2) - 30, 60, 8, parentWindow)
        {
            Create(Message, parentWindow);
        }

        public Confirm(String Message, Window parentWindow, ConsoleColor backgroundColour, String Title = "Message")
            : base(Title, 6, (Console.WindowWidth / 2) - 30, 60, 8, parentWindow)
        {
            BackgroundColour = backgroundColour;

            Create(Message, parentWindow);
        }

        private void Create(String Message, Window parentWindow)
        {
            var messageLabel = new Label(Message, PostionX + 2, PostionY + 2, "messageLabel", this);
            messageLabel.BackgroundColour = BackgroundColour;

            okBtn = new Button(PostionX + 6, PostionY + 2, "OK", "OkBtn", this);
            okBtn.Action = delegate() { Result = true; ExitWindow(); };

            cancelBtn = new Button(PostionX + 6, PostionY + 8, "Cancel", "cancelBtn", this);
            cancelBtn.Action = delegate() { ExitWindow(); };

            Inputs.Add(messageLabel);
            Inputs.Add(okBtn);
            Inputs.Add(cancelBtn);

            CurrentlySelected = okBtn;

            Draw();
            MainLoop();
        }

        
    }
}
