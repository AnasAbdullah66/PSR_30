using System.ComponentModel.DataAnnotations;

namespace PSR_Add_Document.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        //[Required, MaxLength(50, ErrorMessage = “Name cannot exceed 50 characters”)]
        public string CustomerName { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string TinNumber { get; set; }
        public int Gender { get; set; }
        public string Brn { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public DateTime DOB { get; set; }
    }
}
