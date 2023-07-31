using System.ComponentModel.DataAnnotations.Schema;

namespace PSR_Add_Document.Models
{
    public class VMCustomerDocument
    {
        public int CustomerDocumentId { get; set; }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? AccountNumber { get; set; }
        public string TinNumber { get; set; }
        public int AssesmentYear { get; set; }
        public IFormFile Document { get; set; }
        public string? Reference { get; set; }
        public string? RefNumber { get; set; }

        public int? Gender { get; set; }
        public string? Brn { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }

        public DateTime? SubmissionDate { get; set; }
        //public string? SubUser { get; set; }
        //public int? SubUserId { get; set; }
        public int? SubIP { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string? ProcessUser { get; set; }
        public string? ProcessIP { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public string? Location { get; set; }
        public string? BF1 { get; set; }
        public string? BF2 { get; set; }
        public string? BF3 { get; set; }
        public string? BF4 { get; set; }
        public string? BF5 { get; set; }
        public string? BF6 { get; set; }
    }
}
