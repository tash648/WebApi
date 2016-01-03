using Accord.MachineLearning.Structures;
using QuickErrandsWebApi.Models;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace QuickErrandsWebApi.Services.Implementation
{
    [Export(typeof(INearestNeightborService))]
    internal class NearestNeightborService : INearestNeightborService
    {
        public Coordinate GetNearestNeightbor(IEnumerable<Coordinate> candidates, Coordinate core)
        {            
            var points = candidates.Select(p => (double[])p).ToArray();

            var kdTree = KDTree.FromData(points, candidates.ToArray());

            kdTree.Distance = Accord.Math.Distance.Manhattan;

            var result = kdTree.Nearest(core, 3).ToList();            

            return result.FirstOrDefault().Node.Value;
        }
    }
}