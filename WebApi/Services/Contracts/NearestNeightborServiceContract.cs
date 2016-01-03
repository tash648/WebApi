using Google.Maps;
using QuickErrandsWebApi.Models;
using QuickErrandsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickErrands.WebApi.Services.Contracts
{
    [ContractClassFor(typeof(INearestNeightborService))]
    public class NearestNeightborServiceContract : INearestNeightborService
    {
        public Coordinate GetNearestNeightbor(IEnumerable<Coordinate> candidates, Coordinate core)
        {
            Contract.Requires(candidates != null);
            Contract.Requires(core != null);

            throw new NotImplementedException();
        }
    }
}