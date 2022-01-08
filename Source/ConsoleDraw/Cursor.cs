﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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
            WindowManager.WriteText("_", x, y, ConsoleColor.White, background);

            blink = new Timer(500); 
            blink.Elapsed += new ElapsedEventHandler(BlinkCursor);
            blink.Enabled = true;
        }

        public void RemoveCursor()
        {
            if (visible)
            {
                WindowManager.WriteText(" ", _x, _y, ConsoleColor.White, _background);
                if (blink != null)
                    blink.Dispose();
                visible = false;
            }
            
        }

        void BlinkCursor(object sender, ElapsedEventArgs e)
        {
            if (_cursorShow)
            {
                WindowManager.WriteText(blinkLetter.ToString(), _x, _y, ConsoleColor.White, _background);
                _cursorShow = false;
            }
            else
            {
                WindowManager.WriteText("_", _x, _y, ConsoleColor.White, _background);
                _cursorShow = true;
            }
        }


    }
}
