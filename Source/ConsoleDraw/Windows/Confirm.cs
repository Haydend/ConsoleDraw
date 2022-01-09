using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Linq;

namespace ConsoleDraw.Windows
{
    public class Confirm : PopupWindow
    {
        private static int textLength = 46;

        private Button okBtn;
        private Button cancelBtn;
        private DialogResult dr;

        public DialogResult Result = DialogResult.Cancel;

        public Confirm(Window parentWindow, string Message, string Title = "Confirm")
            : base(parentWindow, Title, 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)))
        {
            Create(Message, parentWindow);
        }

        public Confirm(string Message, Window parentWindow, ConsoleColor backgroundColour, string Title = "Message")
            : base(parentWindow, Title, 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)))
        {
            BackgroundColour = backgroundColour;

            Create(Message, parentWindow);
        }

        private void Create(string Message, Window parentWindow)
        {
            int count = 0;
            while ((count * 45) < Message.Count())
            {
                string splitMessage = Message.PadRight(textLength * (count + 1), ' ').Substring((count * textLength), textLength);
                Label messageLabel = new(this, splitMessage, PostionX + 2 + count, PostionY + 2, "messageLabel");
                Inputs.Add(messageLabel);

                count++;
            }

            okBtn = new(this, PostionX + Height - 2, PostionY + 2, "OK", "OkBtn")
            {
                Action = delegate () { ExitWindow(); dr = DialogResult.OK; }
            };

            cancelBtn = new(this, PostionX + Height - 2, PostionY + 8, "Cancel", "cancelBtn")
            {
                Action = delegate () { ExitWindow(); dr = DialogResult.Cancel; }
            };

            Inputs.Add(okBtn);
            Inputs.Add(cancelBtn);

            CurrentlySelected = okBtn;
        }

        public DialogResult ShowDialog()
        {
            Draw();
            MainLoop();

            Result = dr;
            return Result;
        }

    }
}
