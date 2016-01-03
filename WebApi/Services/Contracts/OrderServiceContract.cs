using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using QuickErrandsWebApi.Models;

namespace QuickErrandsWebApi.Services.Contracts
{
    [ContractClassFor(typeof(IOrderService))]
    public class OrderServiceContract : IOrderService
    {
        public bool AddCoordinateToOrder(int orderId, Coordinate coordinate)
        {
            Contract.Requires(orderId >= 0);
            Contract.Requires(coordinate != null);

            throw new NotImplementedException();
        }

        public bool AddOrder(OrderModel orderModel)
        {
            Contract.Requires(orderModel != null);

            throw new NotImplementedException();
        }

        public IEnumerable<Coordinate> GetCoordinatesOfOrder(int orderId)
        {
            Contract.Requires(orderId >= 0);

            throw new NotImplementedException();
        }

        public IEnumerable<OrderModel> GetOrders(string seachPatter)
        {
            Contract.Ensures(Contract.Result<IEnumerable<OrderModel>>() != null);

            throw new NotImplementedException();
        }
    }
}