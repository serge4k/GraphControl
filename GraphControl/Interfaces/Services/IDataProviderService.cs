namespace GraphControl.Interfaces.Services
{
    public interface IDataProviderService : IGraphDataProvider
    {
        uint TestPoints { get; set; }

        void Run();

        void Run(long interval);
    }
}