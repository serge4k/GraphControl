using GraphControlCore.Interfaces.Services;
using GraphControlCore.Interfaces.Views;

namespace GraphControlWinFormsTestApp.Interfaces
{
    internal interface IGraphControlTestFormView : IView, IRegisterDataProvider
    {
        void ShowView();
    }
}
