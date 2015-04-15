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
            : base(Title, 10, 45, 60, 8, parentWindow)
        {
            var messageLabel = new Label(Message, 12, 47, "messageLabel", this);
            
            okBtn = new Button(16, 48, "OK", "OkBtn", this);
            okBtn.Action = delegate() { Result = true; ExitWindow(); };
            cancelBtn = new Button(16, 54, "Cancel", "OkBtn", this);
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
