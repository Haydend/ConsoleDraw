﻿using ConsoleDraw.Inputs;
using ConsoleDraw.Windows.Base;
using System;
using System.Linq;

namespace ConsoleDraw.Windows
{
    public class Alert : PopupWindow
    {
        private Button okBtn;
        private static int textLength = 46;


        public Alert(string Message, Window parentWindow)
            : base("Message", 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)), parentWindow)
        {
            Create(Message, parentWindow);
        }

        public Alert(string Message, Window parentWindow, string Title)
            : base(Title, 6, (Console.WindowWidth / 2) - 30, 25, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)), parentWindow)
        {
            Create(Message, parentWindow);
        }

        public Alert(string Message, Window parentWindow, ConsoleColor backgroundColour)
            : base("Message", 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)), parentWindow)
        {
            BackgroundColour = backgroundColour;

            Create(Message, parentWindow);
        }

        public Alert(string Message, Window parentWindow, ConsoleColor backgroundColour, string Title)
            : base(Title, 6, (Console.WindowWidth / 2) - 25, 50, 5 + (int)Math.Ceiling(((double)Message.Count() / textLength)), parentWindow)
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
                Label messageLabel = new(splitMessage, PostionX + 2 + count, PostionY + 2, "messageLabel", this);
                Inputs.Add(messageLabel);

                count++;
            }

            /*
            Label messageLabel = new Label(Message, PostionX + 2, PostionY + 2, "messageLabel", this);
            messageLabel.BackgroundColour = BackgroundColour;*/

            okBtn = new(PostionX + Height - 2, PostionY + 2, "OK", "OkBtn", this)
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
