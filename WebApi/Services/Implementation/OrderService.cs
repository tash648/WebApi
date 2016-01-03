using QuickErrandsWebApi.Models;
using QuickErrandsWebApi.Repository;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace QuickErrandsWebApi.Services.Implementation
{
    [Export(typeof(IOrderService))]
    public class OrderService : IOrderService
    {
        public bool AddCoordinateToOrder(int orderId, Coordinate coordinate)
        {
            using (var work = new ErrandsUnitOfWork())
            {
                var order = work.GetRepository<OrderModel>().Get(orderId);

                if (order == null)
                    return false;

                order.Coordinates.Add(coordinate);

                work.Commit();
            }

            return true;
        }

        public bool AddOrder(OrderModel orderModel)
        {
            using (var work = new ErrandsUnitOfWork())
            {
                work.GetRepository<OrderModel>().Insert(orderModel);

                work.Commit();
            }

            return true;
        }

        public IEnumerable<Coordinate> GetCoordinatesOfOrder(int orderId)
        {
            using (var work = new ErrandsUnitOfWork())
            {
                var order = work.GetRepository<OrderModel>().Get(orderId);

                if (order == null)
                    return Enumerable.Empty<Coordinate>();

                return order.Coordinates;
            }
        }

        public IEnumerable<OrderModel> GetOrders(string seachPattern)
        {
            using (var work = new ErrandsUnitOfWork())
            {
                var repository = work.GetRepository<OrderModel>();

                if (string.IsNullOrEmpty(seachPattern))
                    return repository.GetAll();

                return repository.GetAll(p => p.AddressFrom.Contains(seachPattern) || p.AddressTo.Contains(seachPattern) || p.Commentary.Contains(seachPattern));
            }
        }
    }
}