using APIarchitecturePractice.Data_Access_Layer;
using APIarchitecturePractice.DTOs;
using APIarchitecturePractice.Models;

namespace APIarchitecturePractice.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRespository orderRepository;
        public CustomerService(ICustomerRepository _customerRepository, IOrderRespository _orderRespository)
        {
            customerRepository = _customerRepository;
            orderRepository = _orderRespository;
        }

        public async Task<List<Customer>> GetAllCustomers_Service()
        {
            return await customerRepository.GetAllCustomers();
        }

        public async Task AddCustomer_Service(AddCustomerDTO addCustomerDTO)
        {
            Customer customer = new Customer
            {
                Customer_Name = addCustomerDTO.Customer_Name,
                Customer_Email = addCustomerDTO.Customer_Email,
                createdAt = DateTime.Now
            };

            try
            {
                await customerRepository.AddCustomer_Repo(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CustomerOrderJoinDTO>> GetCustomerOrder()
        {

            var customersTable = await customerRepository.GetAllCustomers();

            var ordersTable = await orderRepository.GetAllOrders_Repo();


            var join = customersTable.Join(
                ordersTable,
                c => c.Customer_Id,
                o => o.Customer_Id,
                (c, o) => new CustomerOrderJoinDTO
                {
                    Customer_Id = c.Customer_Id,
                    Customer_Name = c.Customer_Name,
                    OrderId = o.Id,
                    Amount = o.Amount,
                    OrderStatus = o.OrderStatus,
                    DeliveryLocation = o.DeliveryLocation,
                }
            ).ToList();

            return join;
        }

        public async Task<ResponseObject<CustomerOrderJoinDTO>> GetCustomerOrder2()
        {
            try
            {
                var customersTable = await customerRepository.GetAllCustomers();
                if (customersTable.Any() == false)
                {
                    return new ResponseObject<CustomerOrderJoinDTO>
                    {
                        Message = "No customer found!",
                        // Returns an empty list
                        CustomerOrdersList = new List<CustomerOrderJoinDTO>()
                    };
                };


                var ordersTable = await orderRepository.GetAllOrders_Repo();
                if (ordersTable.Any() == false)
                {
                    return new ResponseObject<CustomerOrderJoinDTO>
                    {
                        Message = "No order found!",
                        // Returns an empty list
                        CustomerOrdersList = new List<CustomerOrderJoinDTO>()
                    };
                };

                var join = customersTable.Join(
                    ordersTable,
                    c => c.Customer_Id,
                    o => o.Customer_Id,
                    (c, o) => new CustomerOrderJoinDTO
                    {
                        Customer_Id = c.Customer_Id,
                        Customer_Name = c.Customer_Name,
                        OrderId = o.Id,
                        Amount = o.Amount,
                        OrderStatus = o.OrderStatus,
                        DeliveryLocation = o.DeliveryLocation,
                    }
                ).ToList();

                if(join.Any() == false)
                {
                    return new ResponseObject<CustomerOrderJoinDTO>
                    {
                        Message = "No matching orders and customers found!",
                        // Returns an empty list
                        CustomerOrdersList = new List<CustomerOrderJoinDTO>()
                    };
                }

                return new ResponseObject<CustomerOrderJoinDTO>
                {
                    Message = "Joined Customer and Order Table",
                    CustomerOrdersList = join
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<CustomerSpecificOrdersDTO> GetCustomerSpecificOrders_Service(int _customerId)
        {
            var gotCustomer = await customerRepository.GetCustomerById(_customerId);
            var ordersTable = await orderRepository.GetCustomerSpecificOrders_Repo(_customerId);


            CustomerSpecificOrdersDTO customerSpecificOrdersDTO = new CustomerSpecificOrdersDTO
            {
                Customer_Id = gotCustomer.Customer_Id,
                Customer_Name = gotCustomer.Customer_Name,
                Customer_Orders = ordersTable
            };

            return customerSpecificOrdersDTO;
            


          

        }


        public async Task<string> DeleteCustomer_Service(int _customerId)
        {
            try
            {
                string gotMessage = await customerRepository.DeleteCustomer_Repo(_customerId);
                return gotMessage;

            }
            catch(Exception ex)
            {
                throw;
            }
        }
  
        public async Task<Customer> UpdateCustomer_Service(int _customerId, string _customerName)
        {
            var getUpdatedCustomer = await customerRepository.UpdateCustomer_Repo(_customerId, _customerName);
            return getUpdatedCustomer;
        }


        public class ResponseObject<T>
        {
            public string Message { get; set; }
            public List<T> CustomerOrdersList { get; set; }
        }


    }
}
