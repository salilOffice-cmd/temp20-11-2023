namespace APIarchitecturePractice.DTOs
{
    public class CustomerOrderJoinDTO
    {
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public string DeliveryLocation { get; set; }
        public string OrderStatus { get; set; }
    }
}
