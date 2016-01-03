using Google.Maps;
using Google.Maps.Direction;
using QuickErrandsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickErrands.WebApi.Services
{
    public interface IGoogleApiService
    {
        Task<DirectionResponse> GetDuration(Coordinate origins, Coordinate destination, TravelMode travelMode);
    }
}
