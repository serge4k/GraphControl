using GraphControlCore.Events;

namespace GraphControlCore.Interfaces
{
    public interface IGraphDataProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name fixed by interface name requiremenets")]
        event GraphDataHandler OnReceiveData;
    }
}
