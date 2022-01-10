namespace ConsoleDraw.Windows.Base
{
    public abstract class IWindow
    {
        public abstract void ReDraw();

        public Window ParentWindow;
    }
}
