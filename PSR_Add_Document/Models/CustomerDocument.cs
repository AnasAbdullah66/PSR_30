using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSR_Add_Document.Models
{
    public class CustomerDocument
    {
        public int CustomerDocumentId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }
        public string? AccountNumber { get; set; }
        public string TinNumber { get; set; }

        //[ForeignKey("AssesmentYears")]
        //public int AssesmentYearsId { get; set; }
        public int AssesmentYear { get; set; }
        public string? Document { get; set; }
        public string? Reference { get; set; }
        public string? RefNumber { get; set; }
        public DateTime? SubmissionDate { get; set; }
        //[ForeignKey("SubUser")]
        //public int? SubUserId { get; set; }
        public string? SubUser { get; set; }
        public string? SubIP { get; set; }
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

        ////
        //public virtual SubUser SubUser { get; set; }
        public virtual Customer Customer { get; set; }
        //public virtual AssesmentYears AssesmentYears { get; set; }
    }


    //public class SubUser
    //{
    //    public int SubUserId { get; set; }
    //    public string? UserType { get; set; }

    //}

    //public class AssesmentYears
    //{
    //    public int AssesmentYearsId { get; set; }
    //    public int AssesmentYear { get; set; }
    //}


}
