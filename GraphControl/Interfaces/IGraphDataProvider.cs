using GraphControl.Events;

namespace GraphControl.Interfaces
{
    public interface IGraphDataProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name fixed by interface name requiremenets")]
        event GraphDataHandler OnReceiveData;
    }
}
