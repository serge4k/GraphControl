using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Models;

namespace GraphControl.Tests.Services
{
    internal class TestRangesDataProviderService : TestBaseDataProvider
    {
        private double currentX;
        private double currentY;
        private int stage = 0;

        public TestRangesDataProviderService(uint testPoints) : base(testPoints)
        {

        }

        protected override IDataItem GenerateNextValue()
        {
            switch (stage++ % 4)
            {
                case 0: // zero step
                case 1: // zero step
                    break;
                case 2: // small step
                    this.currentX = this.currentX + .1e-307;
                    this.currentY = this.currentY + .1e-307;
                    break;
                case 3: // min, max values
                    this.currentY = this.currentY < double.MaxValue ? double.MaxValue : double.MinValue;
                    this.currentX = this.currentX < double.MaxValue ? double.MaxValue : double.MinValue;
                    break;
                
                
            }
            
            return new DataItem(this.currentX, this.currentY);
        }
    }
}
