using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Linq;

namespace ConsoleDraw.Windows
{
    public class Alert : PopupWindow
    {
        private Button okBtn;
        private static int textLength = 46;


        public Alert(Window parentWindow, string Message)
            : base(parentWindow, "Message", 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)))
        {
            Create(parentWindow, Message);
        }

        public Alert(string Message, Window parentWindow, string Title)
            : base(parentWindow, Title, 6, (Console.WindowWidth / 2) - 30, 25, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)))
        {
            Create(parentWindow, Message);
        }

        public Alert(string Message, Window parentWindow, ConsoleColor backgroundColour)
            : base(parentWindow, "Message", 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)))
        {
            BackgroundColour = backgroundColour;

            Create(parentWindow, Message);
        }

        public Alert(string Message, Window parentWindow, ConsoleColor backgroundColour, string Title)
            : base(parentWindow, Title, 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)))
        {
            BackgroundColour = backgroundColour;

            Create(parentWindow, Message);
        }

        private void Create(Window parentWindow, string Message)
        {
            int count = 0;
            while ((count * 45) < Message.Count())
            {
                string splitMessage = Message.PadRight(textLength * (count + 1), ' ').Substring((count * textLength), textLength);
                Label messageLabel = new(this, splitMessage, PostionX + 2 + count, PostionY + 2, "messageLabel");
                Inputs.Add(messageLabel);

                count++;
            }

            /*
            Label messageLabel = new Label(Message, PostionX + 2, PostionY + 2, "messageLabel", this);
            messageLabel.BackgroundColour = BackgroundColour;*/

            okBtn = new(this, PostionX + Height - 2, PostionY + 2, "OK", "OkBtn")
            {
                Action = delegate () { ExitWindow(); }
            };

            Inputs.Add(okBtn);

            CurrentlySelected = okBtn;

            Draw();
            MainLoop();
        }
    }
}
