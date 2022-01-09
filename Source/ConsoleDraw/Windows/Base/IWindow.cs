namespace ConsoleDraw.Windows.Base
{
    public abstract class IWindow
    {
        abstract public void ReDraw();

        public Window ParentWindow;
    }
}
