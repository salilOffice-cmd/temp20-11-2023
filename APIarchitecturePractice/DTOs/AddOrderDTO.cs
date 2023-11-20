namespace APIarchitecturePractice.DTOs
{
    public class AddOrderDTO
    {
        public int Customer_Id { get; set; }
        public int Amount { get; set; }
        public string DeliveryLocation { get; set; }
    }
}
