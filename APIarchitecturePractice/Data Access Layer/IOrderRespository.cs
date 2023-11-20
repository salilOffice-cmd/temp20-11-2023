using APIarchitecturePractice.Models;

namespace APIarchitecturePractice.Data_Access_Layer
{
    public interface IOrderRespository
    {
        Task AddOrder_Repo(Order order);
        Task<List<Order>> GetAllOrders_Repo();

        Task<List<Order>> GetCustomerSpecificOrders_Repo(int _customerId);
    }
}
