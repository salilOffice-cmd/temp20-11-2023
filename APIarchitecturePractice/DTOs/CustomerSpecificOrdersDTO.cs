using APIarchitecturePractice.Models;

namespace APIarchitecturePractice.DTOs
{
    public class CustomerSpecificOrdersDTO
    {
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }

        public List<Order> Customer_Orders { get; set; }
    }
}
