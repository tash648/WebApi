using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using QuickErrandsWebApi.Models;
using MSoft.AsyncResponces;
using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.Direction;
using System.Globalization;

namespace QuickErrands.WebApi.Services.Implementation
{
    [Export(typeof(IGoogleApiService))]
    public class GoogleApiService : WebApiClient, IGoogleApiService
    {
        public Task<DirectionResponse> GetDuration(Coordinate origins, Coordinate destination, TravelMode travelMode)
        {
            var request = new DirectionRequest() { Origin = new Location(origins.ToString()), Destination = new Location(destination.ToString()), Mode = travelMode, Language = "ru-RU", Sensor = false, ApiKey = "AIzaSyBn_9_tGXpdsLRM0P5utPbIZH8CT3q42Ow"};

            var directionService = new DirectionService();

            var responceTask = GetResponceResult<DirectionResponse>(directionService.GetRequestUriWithBaseUri(request).ToString());

            responceTask.ConfigureAwait(false);

            return responceTask;
        }
    }
}