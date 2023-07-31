using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSR_Add_Document.Models
{
    public class OTPManage
    {
        [Key]
        public int OTPId { get; set; }
        public string? OTP { get; set; }
        public DateTime? OtpCreateTime { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OtpLastingTime { get; set; }
        public string MobileNumber { get; set; }
        public string IPADDRESS { get; set;}
        public string AccountNumber { get; set;}
    }
}
