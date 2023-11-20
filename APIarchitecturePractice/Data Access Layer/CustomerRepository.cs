using APIarchitecturePractice.Contexts;
using APIarchitecturePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace APIarchitecturePractice.Data_Access_Layer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DeliveryDBContext context;
        public CustomerRepository(DeliveryDBContext deliveryDBContext)
        {
            context = deliveryDBContext;
        }

        public async Task AddCustomer_Repo(Customer customer)
        {
            try
            {
                //throw new Exception("Customer not added due to database error");
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();

            }

            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                //throw new Exception("Error returning customers from database");
                var customersList = await context.Customers.ToListAsync();
                return customersList;

            }

            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Customer> GetCustomerById(int _customerId)
        {
            try
            {
                var gotCustomer = await context.Customers
                    .FirstOrDefaultAsync(c => c.Customer_Id == _customerId);

                // To know the difference between
                // FirstOrDefaultAsync() & SingleOrDefaultAsync() vs FindAsync()
                // https://stackoverflow.com/questions/54819705/firstordefaultasync-singleordefaultasync-vs-findasync-efcore

                return gotCustomer;

            }
            catch(Exception ex)
            {
                throw;
            }
        }



        public async Task<string> DeleteCustomer_Repo(int _customerId)
        {
            try
            {
                //throw new Exception("Customer not deleted due to database error");

                Customer foundCustomer = await context.Customers.FindAsync(_customerId);
  
                 if(foundCustomer == null) return "Customer with the given ID not found";
  
                 //context.Customers.ExecuteDeleteAsync(c => c.);
                 context.Customers.Remove(foundCustomer);
                 await context.SaveChangesAsync();
                 return "Customer deleted succesfully!";
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
  
  
         public async Task<Customer> UpdateCustomer_Repo(int customerId, string _customerName)
         {
             var getcustomer = await context.Customers.FindAsync(customerId);
  
             if(getcustomer == null) return null;
  
             if(getcustomer != null)
             {
                getcustomer.Customer_Name = _customerName;
                context.SaveChanges();
             }
  
             return getcustomer;
         }


    }
}
