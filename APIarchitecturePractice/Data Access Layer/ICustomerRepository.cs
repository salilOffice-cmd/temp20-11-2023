using APIarchitecturePractice.Models;

namespace APIarchitecturePractice.Data_Access_Layer
{
    public interface ICustomerRepository
    {
        Task AddCustomer_Repo (Customer customer);
        Task<List<Customer>> GetAllCustomers();

        Task<Customer> GetCustomerById(int _customerId);

        Task<string> DeleteCustomer_Repo(int _customerId);

        Task<Customer> UpdateCustomer_Repo(int customerId, string _customerName);

    }
}
