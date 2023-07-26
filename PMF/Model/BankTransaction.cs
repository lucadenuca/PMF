using PMF.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMF.Model
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public string BeneficiaryName { get; set; }
        public DateTime Date { get; set; }
        public string Direction { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string? Mcc { get; set; }
        public string Kind { get; set; }
        [ForeignKey("CatCode")]
        public string CatCode { get; set; } //dodato 
        
        public CategoryEntity CategoryEntity { get; set; } // dodato
    }
}
