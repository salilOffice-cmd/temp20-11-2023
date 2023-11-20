using APIarchitecturePractice.DTOs;
using APIarchitecturePractice.Models;

namespace APIarchitecturePractice.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders_Service();
        Task AddOrder_Service(AddOrderDTO dto);

        Task<List<OrdersGroupByLocationDTO>> GroupOrdersByLocation_Service();
    }
}
