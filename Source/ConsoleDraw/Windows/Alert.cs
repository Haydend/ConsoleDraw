using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw.Windows
{
    public class Alert : PopupWindow
    {
        private Button okBtn;

        public Alert(String Message, Window parentWindow)
            : base("Message", 6, (Console.WindowWidth / 2) - 30, 60, 8, parentWindow)
        {
            Create(Message, parentWindow);
        }

        public Alert(String Message, Window parentWindow, String Title)
            : base(Title, 6, (Console.WindowWidth/2)-30, 60, 8, parentWindow)
        {
            Create(Message, parentWindow);
        }

        public Alert(String Message, Window parentWindow, ConsoleColor backgroundColour)
            : base("Message", 6, (Console.WindowWidth / 2) - 30, 60, 8, parentWindow)
        {
            BackgroundColour = backgroundColour;

            Create(Message, parentWindow);
        }

        public Alert(String Message, Window parentWindow, ConsoleColor backgroundColour, String Title)
            : base(Title, 6, (Console.WindowWidth / 2) - 30, 60, 8, parentWindow)
        {
            BackgroundColour = backgroundColour;

            Create(Message, parentWindow);
        }

        private void Create(String Message, Window parentWindow)
        {
            var messageLabel = new Label(Message, PostionX + 2, PostionY + 2, "messageLabel", this);
            messageLabel.BackgroudColour = BackgroundColour;

            okBtn = new Button(PostionX + 6, PostionY + 2, "OK", "OkBtn", this);
            okBtn.BackgroundColour = BackgroundColour;
            okBtn.Action = delegate() { ExitWindow(); };

            Inputs.Add(messageLabel);
            Inputs.Add(okBtn);

            CurrentlySelected = okBtn;

            Draw();
            MainLoop();
        }
    }
}
