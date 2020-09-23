using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphControl.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Tests.Services;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Models;
using GraphControl.Core.Definitions;
using GraphControl.Core.Exceptions;

namespace GraphControl.Core.Services.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var obj = new DataService();
            Assert.IsTrue(obj is IDataService, $"Type of obj is {obj.GetType().ToString()}");
        }

        [TestMethod()]
        public void MaxPointsLimitTest()
        {
            {
                Assert.ThrowsException<InvalidArgumentException>(() => RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                    null,
                    out DataService service,
                    out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                    out int dataItemsGeneratedRes,
                    1, 0));
            }

            {
                RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                    null,
                    out DataService service,
                    out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                    out int dataItemsGeneratedRes,
                    1000, 500);

                Assert.IsTrue(service.ItemCount == maxPoints, $"service.ItemCount {service.ItemCount} not equals {maxPoints}");
                Assert.IsTrue(dataUpdatedNotifications == maxPoints, $"dataUpdatedNotifications {dataUpdatedNotifications} not equals {maxPoints}");
                var itemsInService = service.GetItems(double.MinValue, double.MaxValue);
                Assert.IsTrue(itemsInService.Count() == maxPoints, $"service.GetItems(double.MinValue, double.MaxValue).Count() {itemsInService.Count()} is not equal maxPoints {maxPoints}");
            }
        }

        private static void RunServiceAndProvider(out int maxPointsRes,
            out int dataUpdatedNotificationsRes, List<IDataItem> generatedPointList,
            out DataService service,
            out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
            out int dataItemsGeneratedRes)
        {
            RunServiceAndProvider(out maxPointsRes, 
                out dataUpdatedNotificationsRes,
                generatedPointList,
                out service,
                out minXRes, out maxXRes, out minYRes, out maxYRes,
                out dataItemsGeneratedRes,
                100, 100000);
        }

        private static void RunServiceAndProvider(out int maxPointsRes,
            out int dataUpdatedNotificationsRes, List<IDataItem> generatedPointList,
            out DataService service,
            out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
            out int dataItemsGeneratedRes,
            int linesMumber, int maxPoints)
        {
            var pointsNumber = linesMumber + 1;

            var dataUpdatedNotificationsSink = new object();
            var dataUpdatedNotifications = 0;

            var sink = new object();
            var minX = double.MaxValue;
            var maxX = double.MinValue;
            var minY = double.MaxValue;
            var maxY = double.MinValue;
            int dataItemsGenerated = 0;

            using (var provider = new TestSinusDataProviderService())
            {
                provider.TestPoints = (uint)pointsNumber;

                provider.OnReceiveData += (value, dateTime)
                    =>
                {
                    lock (sink)
                    {
                        var item = new DataItem(dateTime.Ticks / TimeSpan.TicksPerMillisecond, value);
                        minX = minX > item.X ? item.X : minX;
                        maxX = maxX < item.X ? item.X : maxX;
                        minY = minY > item.Y ? item.Y : minY;
                        maxY = maxY < item.Y ? item.Y : maxY;
                        dataItemsGenerated++;
                    }                    
                };
                provider.OnGraphDataArrayReceived += (sender, e)
                    =>
                {
                    lock (sink)
                    {
                        foreach (var item in e.DataItems)
                        {
                            minX = minX > item.X ? item.X : minX;
                            maxX = maxX < item.X ? item.X : maxX;
                            minY = minY > item.Y ? item.Y : minY;
                            maxY = maxY < item.Y ? item.Y : maxY;
                        }
                        dataItemsGenerated += e.DataItems.Count;
                        if (e.DataItems.Count - maxPoints - 1 < 0)
                        {
                            generatedPointList?.AddRange(e.DataItems);
                        }
                        else
                        {
                            generatedPointList?.AddRange(
                                e.DataItems.ToList()
                                .GetRange(e.DataItems.Count - maxPoints - 1, maxPoints));
                        }
                        
                    }
                };

                service = new DataService((uint)maxPoints);
                service.DataUpdated += (sender, e) =>
                {
                    lock (dataUpdatedNotificationsSink)
                    {
                        dataUpdatedNotifications += e.Items.Count;
                    }                    
                };
                service.RegisterDataProvider(provider);

                provider.Run();
            }
            maxPointsRes = maxPoints;
            dataUpdatedNotificationsRes = dataUpdatedNotifications;
            minXRes = minX;
            maxXRes = maxX;
            minYRes = minY;
            maxYRes = maxY;
            dataItemsGeneratedRes = dataItemsGenerated;
        }

        [TestMethod()]
        public void RegisterDataProviderTest()
        {
            RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                null,
                out DataService service,
                out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                out int dataItemsGeneratedRes);

            Assert.IsTrue(dataUpdatedNotifications == dataItemsGeneratedRes, $"dataUpdatedNotifications {dataUpdatedNotifications} not equals dataItemsGeneratedRes {dataItemsGeneratedRes}");
        }

        [TestMethod()]
        public void GetMinTest()
        {
            RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                null,
                out DataService service,
                out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                out int dataItemsGeneratedRes);

            Assert.IsTrue(service.GetMin(Axis.X) == minXRes, $"service.GetMin(Axis.X) {service.GetMin(Axis.X)} is not equal {minXRes} (generated {dataItemsGeneratedRes}, received {dataUpdatedNotifications})");
            Assert.IsTrue(service.GetMin(Axis.Y) == minYRes, $"service.GetMin(Axis.Y) {service.GetMin(Axis.Y)} is not equal {minYRes} (generated {dataItemsGeneratedRes}, received {dataUpdatedNotifications})");
        }

        [TestMethod()]
        public void DisposeTest()
        {
            RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                null,
                out DataService service,
                out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                out int dataItemsGeneratedRes);

            Assert.IsTrue(dataItemsGeneratedRes == dataUpdatedNotifications, $"generated {dataItemsGeneratedRes} != received {dataUpdatedNotifications})");
        }

        [TestMethod()]
        public void GetMaxTest()
        {
            RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                null,
                out DataService service,
                out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                out int dataItemsGeneratedRes);

            Assert.IsTrue(service.GetMax(Axis.X) == maxXRes, $"service.GetMax(Axis.X) {service.GetMax(Axis.X)} is not equal {maxXRes} (generated {dataItemsGeneratedRes}, received {dataUpdatedNotifications})");
            Assert.IsTrue(service.GetMax(Axis.Y) == maxYRes, $"service.GetMax(Axis.Y) {service.GetMax(Axis.Y)} is not equal {maxYRes} (generated {dataItemsGeneratedRes}, received {dataUpdatedNotifications})");
        }

        [TestMethod()]
        public void GetItemsTest()
        {
            List<IDataItem> generatedPointList = new List<IDataItem>();

            RunServiceAndProvider(out int maxPoints, out int dataUpdatedNotifications,
                generatedPointList,
                out DataService service,
                out double minXRes, out double maxXRes, out double minYRes, out double maxYRes,
                out int dataItemsGeneratedRes);

            var itemsInService = service.GetItems(double.MinValue, double.MaxValue);
            Assert.IsTrue(itemsInService.SequenceEqual(generatedPointList), $"service.Items {itemsInService.Count()} is not equals with {generatedPointList.Count}");
        }
    }
}