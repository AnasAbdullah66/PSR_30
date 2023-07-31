using System.ComponentModel.DataAnnotations;

namespace PSR_Add_Document.ViewModel
{
    public class VMCustomer
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }
       
        public string? AccountNumber { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? TinNumber { get; set; }
        public int Gender { get; set; }
        public string? Brn { get; set; }
        
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? IsMobNo { get; set; }
        public string? IsEmail { get; set; }
        public string? ErrorShow { get; set; }

    }
}
