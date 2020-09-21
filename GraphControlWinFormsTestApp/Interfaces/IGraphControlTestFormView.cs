using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;

namespace GraphControlWinFormsTestApp.Interfaces
{
    internal interface IGraphControlTestFormView : IView, IRegisterDataProvider
    {
        void ShowView();
    }
}
