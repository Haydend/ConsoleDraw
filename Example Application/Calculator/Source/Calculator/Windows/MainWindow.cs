using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDraw.Windows.Base;
using ConsoleDraw.Inputs;
using ConsoleDraw;

namespace Calculator.Windows
{
    public class MainWindow : FullWindow
    {
        TextBox Display;

        Double Total = 0;
        char LastOp = '=';
        bool DisplayingTotal = true;
        bool PointUsed = false;

        public MainWindow() : base(0, 0, Console.WindowWidth, Console.WindowHeight, null)
        {
            Display = new TextBox(2, 3, "0", "displayTxtBox", this, 21) { Selectable = false };

            Button PointBtn = new Button(13, 8, " . ", "pointBtn", this) { Action = delegate() { AddPoint(); } };
            Button ClearBtn = new Button(5, 2, " C ", "clearBtn", this) { Action = delegate() { Clear(); } };

            Button ZeroBtn = new Button(13, 2, " 0 ", "zeroBtn", this) { Action = delegate() { Number('0'); } };
            Button OneBtn = new Button(11, 2, " 1 ", "oneBtn", this) { Action = delegate() { Number('1'); } };
            Button TwoBtn = new Button(11, 8, " 2 ", "twoBtn", this) { Action = delegate() { Number('2'); } };
            Button ThreeBtn = new Button(11, 14, " 3 ", "threeBtn", this) { Action = delegate() { Number('3'); } };
            Button FourBtn = new Button(9, 2, " 4 ", "fourBtn", this) { Action = delegate() { Number('4'); } };
            Button FiveBtn = new Button(9, 8, " 5 ", "fiveBtn", this) { Action = delegate() { Number('5'); } };
            Button SixBtn = new Button(9, 14, " 6 ", "sixBtn", this) { Action = delegate() { Number('6'); } };
            Button SevenBtn = new Button(7, 2, " 7 ", "sevenBtn", this) { Action = delegate() { Number('7'); } };
            Button EightBtn = new Button(7, 8, " 8 ", "eightBtn", this) { Action = delegate() { Number('8'); } };
            Button NineBtn = new Button(7, 14, " 9 ", "nineBtn", this) { Action = delegate() { Number('9'); } };


            var minusBtn = new Button(5, 20, " - ", "minusBtn", this) { Action = delegate() { Operator('-'); } };
            var addBtn = new Button(7, 20, " + ", "addBtn", this) { Action = delegate() { Operator('+'); } };
            var timesBtn = new Button(9, 20, " x ", "timesBtn", this) { Action = delegate() { Operator('*'); } };
            var divideBtn = new Button(11, 20, " / ", "divideBtn", this) { Action = delegate() { Operator('/'); } };
            var equalsBtn = new Button(13, 14, "    =    ", "equalsBtn", this) { Action = delegate() { Operator('='); } };

            Inputs.Add(Display);

            Inputs.Add(ClearBtn);
            Inputs.Add(minusBtn);

            Inputs.Add(SevenBtn);
            Inputs.Add(EightBtn);
            Inputs.Add(NineBtn);
            Inputs.Add(addBtn);

            Inputs.Add(FourBtn);
            Inputs.Add(FiveBtn);
            Inputs.Add(SixBtn);
            
            Inputs.Add(timesBtn);

            Inputs.Add(OneBtn);
            Inputs.Add(TwoBtn);
            Inputs.Add(ThreeBtn);
            
            Inputs.Add(divideBtn);
            
            Inputs.Add(ZeroBtn);
            Inputs.Add(PointBtn);
            Inputs.Add(equalsBtn);

            CurrentlySelected = OneBtn;
            Draw();
            MainLoop();
        }

        public override void ReDraw()
        {
            //Black Boarder
            WindowManager.DrawColourBlock(ConsoleColor.DarkGray, 1, 2, 4, 25); 
        }

        private void Number(char number)
        { 
            if(DisplayingTotal)
                Display.SetText(number.ToString());
            else
                Display.SetText(Display.GetText() + number);

            DisplayingTotal = false;
        }

        private void AddPoint()
        {
            if (PointUsed) //Number already has a point
                return;

            if (DisplayingTotal)
                Display.SetText("0.");
            else
                Display.SetText(Display.GetText() + '.');

            DisplayingTotal = false;
            PointUsed = true;
        }

        private void Clear()
        {
            LastOp = '=';
            Total = 0;
            Display.SetText("0");
            DisplayingTotal = true;
        }   

        private void Operator(char op)
        {
            
            Double number = 0;

            if(Display.GetText() != "")
                number = Double.Parse(Display.GetText());

            if (LastOp == '-')
                Total = Total - number;
            else if (LastOp == '+')
                Total = Total + number;
            else if (LastOp == '/')
                Total = Total / number;
            else if (LastOp == '*')
                Total = Total * number;
            else if (LastOp == '=')
                Total = number;

            Display.SetText(Total.ToString());
            DisplayingTotal = true;
            PointUsed = false;

            LastOp = op;
        }
    }
}
