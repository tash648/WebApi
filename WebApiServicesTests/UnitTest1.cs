using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition.Hosting;
using QuickErrandsWebApi.Services;
using QuickErrandsWebApi.Models;
using QuickErrands.WebApi.Services;

namespace WebApiServicesTests
{
    [TestClass]
    public class NearestNeightborServiceTests
    {
        private CompositionContainer container;

        public NearestNeightborServiceTests()
        {
            container = new CompositionContainer(new AssemblyCatalog(typeof(INearestNeightborService).Assembly));
        }

        [TestMethod]
        public void GetNearestNeightborTest()
        {
            var service = container.GetExportedValue<INearestNeightborService>();
            var google = container.GetExportedValue<IGoogleApiService>();

            var candidates = new[]
            {
                new Coordinate(47.2231,39.7025),
                new Coordinate(47.2231, 39.7014),
                new Coordinate(47.2213, 39.7058),
                new Coordinate(47.2227, 39.7012),
                new Coordinate(47.2292, 39.6938),
                new Coordinate(47.2332, 39.7139),
                new Coordinate(47.2444, 39.7132)
            };

            var core = new Coordinate(47.2229, 39.7025);

            var nearest = service.GetNearestNeightbor(candidates, core);

            google.GetDuration(nearest, core);

            Assert.IsTrue(nearest.Latitude == 47.2231 && nearest.Longitude == 39.7025);
        }
    }
}
