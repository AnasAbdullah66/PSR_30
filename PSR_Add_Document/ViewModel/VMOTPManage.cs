namespace PSR_Add_Document.ViewModel
{
    public class VMOTPManage
    {
        public int OTPId { get; set; }
        public string? OTP { get; set; }
        public DateTime? OtpCreateTime { get; set; }
        //public int? CustomerId { get; set; }
        public DateTime? OtpLastingTime { get; set; }
        public string? MobileNumber { get; set; }
        public string? IPADDRESS { get; set; }
        public string? AccountNumber { get; set; }
    }
}
