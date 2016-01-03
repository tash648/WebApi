using System.ComponentModel.Composition;
using System.Web.Http;
using QuickErrandsWebApi.Attributes;
using QuickErrandsWebApi.BindingModels;
using QuickErrandsWebApi.Models;
using QuickErrandsWebApi.Models.Roles;
using QuickErrandsWebApi.Services;
using QuickErrands.WebApi.Services;
using System.Threading.Tasks;

namespace QuickErrandsWebApi.Controllers
{
    [Export]
    [RoutePrefix("api/v1/orders")]
    public class OrderController : ApiController
    {
        [Import]
        private IOrderService OrderService { get; set; }

        [Import]
        private IGoogleApiService GoogleApiService { get; set; }

        [RoleAuthorize(typeof(Admin))]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await GoogleApiService.GetDuration(new Coordinate(47.2444, 39.7132), new Coordinate(47.2229, 39.7025), Google.Maps.TravelMode.walking);

            return Ok(OrderService.GetOrders(null));
        }

        [RoleAuthorize(typeof(Admin))]
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(OrderModel order)
        {
            return Ok(OrderService.AddOrder(order));
        }

        [RoleAuthorize(typeof(Admin))]
        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(UpdateOrderModel updateOrderModel)
        {
            if (OrderService.AddCoordinateToOrder(updateOrderModel.OrderId, updateOrderModel.Coordinate))
                return Ok();

            return BadRequest();
        }
    }
}
