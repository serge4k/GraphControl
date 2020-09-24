namespace GraphControl.Core.Interfaces.Services
{
    public interface IRegisterDataProvider
    {
        /// <summary>
        /// Registers external data provider
        /// </summary>
        /// <param name="dataProvider">data provider</param>
        void RegisterDataProvider(IGraphDataProvider dataProvider);
    }
}
