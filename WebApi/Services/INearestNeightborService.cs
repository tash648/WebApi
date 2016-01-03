using QuickErrandsWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickErrandsWebApi.Services
{
    public interface INearestNeightborService
    {
        Coordinate GetNearestNeightbor(IEnumerable<Coordinate> candidates, Coordinate core);
    }
}
