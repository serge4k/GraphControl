namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<in TArg>
    {
        void Run(TArg argument);
    }

    public interface IPresenter<in TArg0, in TArg1>
    {
        void Run(TArg0 arg0, TArg1 arg1);
    }
}
