using ConsoleDraw.Windows.Base;

namespace ConsoleDraw.Inputs.Base
{
    public abstract class IInput
    {
        abstract public void Draw();

        public abstract void Select();
        public abstract void Unselect();

        public abstract void AddLetter(char letter);
        public abstract void BackSpace();
        public abstract void CursorMoveLeft();
        public abstract void CursorMoveUp();
        public abstract void CursorMoveDown();
        public abstract void CursorMoveRight();
        public abstract void CursorToStart();
        public abstract void CursorToEnd();
        public abstract void Enter();
        public abstract void Tab();

        public int Xpostion;
        public int Ypostion;
        public int Width;
        public int Height;

        public bool Selectable { get; set; }

        public string ID;
        public Window ParentWindow;
    }
}
