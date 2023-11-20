using APIarchitecturePractice.DTOs;
using APIarchitecturePractice.Models;
using static APIarchitecturePractice.Services.CustomerService;

namespace APIarchitecturePractice.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers_Service();
        Task AddCustomer_Service(AddCustomerDTO dto);
        Task<List<CustomerOrderJoinDTO>> GetCustomerOrder();
        Task<ResponseObject<CustomerOrderJoinDTO>> GetCustomerOrder2();

        Task<CustomerSpecificOrdersDTO> GetCustomerSpecificOrders_Service(int _customerId);

        Task<string> DeleteCustomer_Service(int _customerId);

        Task<Customer> UpdateCustomer_Service(int _customerId, string _customerName);

    }
}
