using ConsoleDraw.Windows.Base;

namespace ConsoleDraw.Inputs.Base
{
    public class Input : IInput
    {
        public Input(int xPostion, int yPostion, int height, int width, Window parentWindow, string iD)
        {
            ParentWindow = parentWindow;
            ID = iD;

            Xpostion = xPostion;
            Ypostion = yPostion;

            Height = height;
            Width = width;
        }

        public override void AddLetter(char letter) { }
        public override void BackSpace() { }
        public override void CursorMoveLeft() { }
        public override void CursorMoveRight() { }
        public override void CursorMoveUp() { }
        public override void CursorMoveDown() { }
        public override void CursorToStart() { }
        public override void CursorToEnd() { }
        public override void Enter() { }
        public override void Tab()
        {
            ParentWindow.MoveToNextItem();
        }

        public override void Unselect() { }
        public override void Select() { }
        public override void Draw() { }
    }
}
