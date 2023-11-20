using APIarchitecturePractice.Data_Access_Layer;
using APIarchitecturePractice.DTOs;
using APIarchitecturePractice.Models;
using APIarchitecturePractice.Data_Access_Layer;
using APIarchitecturePractice.Services;

namespace APIarchitecturePractice.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRespository orderRespository;
        public OrderService(IOrderRespository _orderRespository)
        { 
            orderRespository = _orderRespository;
        }

        public async Task<List<Order>> GetAllOrders_Service()
        {
            return await orderRespository.GetAllOrders_Repo();
        }

        public async Task AddOrder_Service(AddOrderDTO addOrderDTO)
        {
            Order newOrder = new Order
            {
                Customer_Id = addOrderDTO.Customer_Id,
                Amount = addOrderDTO.Amount,
                DeliveryLocation = addOrderDTO.DeliveryLocation,
                OrderStatus = "Initiated",
                Order_date = DateTime.Now,
                EstimatedDeliveryDate = DateTime.Now.AddDays(5)
            };
            //throw new Exception("Something went wrong in AddOrder_Service()");
            await orderRespository.AddOrder_Repo(newOrder);
        }


        public async Task<List<OrdersGroupByLocationDTO>> GroupOrdersByLocation_Service()
        {
            var allOrders = await orderRespository.GetAllOrders_Repo();

            var groupOfOrders = allOrders
                            .GroupBy(order => order.DeliveryLocation)
                            .Select(ordGroup => new OrdersGroupByLocationDTO()
                            {
                                DeliveryLocation = ordGroup.Key,
                                OrdersByLocation = ordGroup.ToList()
                            }).ToList();

            return groupOfOrders;
        }



    }
}
