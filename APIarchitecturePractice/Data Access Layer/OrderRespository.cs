using APIarchitecturePractice.Contexts;
using APIarchitecturePractice.Models;
using APIarchitecturePractice.Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

namespace APIarchitecturePractice.Data_Access_Layer
{
    public class OrderRespository : IOrderRespository
    {
        private readonly DeliveryDBContext context;
        public OrderRespository(DeliveryDBContext ordersDBContext)
        {
            context = ordersDBContext;
        }

        public async Task AddOrder_Repo(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrders_Repo()
        {
            var ordersList = await context.Orders.ToListAsync();
            return ordersList;
        }


        public async Task<List<Order>> GetCustomerSpecificOrders_Repo(int _customerId)
        {
            // To perform asynchronous filtering with Entity Framework Core,
            // you typically use the Where() method with ToListAsync(), 
            var ordersList = await context.Orders
                .Where(o => o.Customer_Id == _customerId).ToListAsync();
            return ordersList;
        }
    }
}
