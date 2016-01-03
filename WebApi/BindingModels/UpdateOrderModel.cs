using QuickErrandsWebApi.Models;

namespace QuickErrandsWebApi.BindingModels
{
    public class UpdateOrderModel
    {
        public int OrderId { get; set; }

        public Coordinate Coordinate { get; set; }
    }
}