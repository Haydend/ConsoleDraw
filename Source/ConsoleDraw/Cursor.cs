using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDraw
{
    public class Cursor
    {
        public bool _cursorShow;
        public int _x;
        public int _y;
        public Timer blink;
        public Char blinkLetter;
        public ConsoleColor _background;
        private bool visible;

        public void PlaceCursor(int x, int y, char letter, ConsoleColor background = ConsoleColor.Blue)
        {
            visible = true;
            _x = x;
            _y = y;
            blinkLetter = letter == '\r' || letter == '\n' ? ' ' : letter;
            _background = background;
            WindowManager.WirteText("_", x, y, ConsoleColor.White, background);

            blink = new Timer(BlinkCursor, null, 500, 500); 
        }

        public void RemoveCursor()
        {
            if (visible)
            {
                WindowManager.WirteText(" ", _x, _y, ConsoleColor.White, _background);
                if (blink != null)
                    blink.Dispose();
                visible = false;
            }
            
        }

        void BlinkCursor(object state)
        {
            if (_cursorShow)
            {
                WindowManager.WirteText(blinkLetter.ToString(), _x, _y, ConsoleColor.White, _background);
                _cursorShow = false;
            }
            else
            {
                WindowManager.WirteText("_", _x, _y, ConsoleColor.White, _background);
                _cursorShow = true;
            }
        }


    }
}
