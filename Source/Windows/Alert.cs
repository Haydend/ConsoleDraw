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

        public Alert(Window parentWindow, String Message, String Title = "Message")
            : base(Title, 10, 45, 60, 8, parentWindow)
        {
            var messageLabel = new Label(Message, 12, 47, "messageLabel", this);
            
            okBtn = new Button(16, 48, "OK", "OkBtn", this);
            okBtn.Action = delegate() { ExitWindow(); };

            Inputs.Add(messageLabel);
            Inputs.Add(okBtn);

            CurrentlySelected = okBtn;

            Draw();
            MainLoop();
        }
    }
}
