using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;

namespace GraphControlWinFormsTestApp.Interfaces
{
    internal interface IGraphControlTestFormView : IView, IRegisterDataProvider
    {
        void ShowView();
    }
}
