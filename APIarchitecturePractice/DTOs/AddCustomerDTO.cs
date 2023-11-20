using System.ComponentModel.DataAnnotations;

namespace APIarchitecturePractice.DTOs
{
    public class AddCustomerDTO
    {
        [Required(ErrorMessage = "EmailAddress is Required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Customer_Email { get; set; }

        [Required(ErrorMessage = "Customer_Name is Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage ="Customer_Name should be 3 to 100 characters long")]
        public string Customer_Name { get; set; }

    }
}
