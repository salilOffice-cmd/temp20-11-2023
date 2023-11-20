using APIarchitecturePractice.Models;
using System.ComponentModel.DataAnnotations;

namespace APIarchitecturePractice.DTOs
{
    public class OrdersGroupByLocationDTO
    {
        public string DeliveryLocation { get; set; }
      
        public List<Order> OrdersByLocation { get; set; }
    }
}
