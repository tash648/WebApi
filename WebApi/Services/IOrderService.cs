using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Http;
using QuickErrandsWebApi.Models;
using QuickErrandsWebApi.Services.Contracts;

namespace QuickErrandsWebApi.Services
{
    [ContractClass(typeof(OrderServiceContract))]
    public interface IOrderService
    {
        IEnumerable<OrderModel> GetOrders(string seachPatter);

        bool AddOrder(OrderModel orderModel);

        bool AddCoordinateToOrder(int orderId, Coordinate coordinate);

        IEnumerable<Coordinate> GetCoordinatesOfOrder(int orderId);
    }
}