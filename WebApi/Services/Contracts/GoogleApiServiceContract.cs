using Google.Maps;
using Google.Maps.Direction;
using QuickErrandsWebApi.Models;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace QuickErrands.WebApi.Services.Contracts
{
    [ContractClassFor(typeof(IGoogleApiService))]
    public class GoogleApiServiceContract : IGoogleApiService
    {
        public Task<DirectionResponse> GetDuration(Coordinate origins, Coordinate destination, TravelMode travelMode)
        {
            Contract.Requires(origins != null);
            Contract.Requires(destination != null);

            throw new NotImplementedException();
        }
    }
}