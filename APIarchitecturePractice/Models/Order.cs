using System.ComponentModel.DataAnnotations;

namespace APIarchitecturePractice.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Amount { get; set; }
        public string DeliveryLocation { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Order_date { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
