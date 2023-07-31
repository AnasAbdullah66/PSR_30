using System.ComponentModel.DataAnnotations;

namespace PSR_Add_Document.Models
{
    public class SessionCustomer
    {
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public int Gender { get; set; }
        public string Brn { get; set; }
        public string? Email { get; set; }
        public DateTime DOB { get; set; }
    }


}
