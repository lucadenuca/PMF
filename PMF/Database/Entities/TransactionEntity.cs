using System.ComponentModel.DataAnnotations.Schema;

namespace PMF.Database.Entities
{
    public class TransactionEntity
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
       /* public string CatCode { get; set; } //dodato 
        [ForeignKey("CatCode")]
        public CategoryEntity CategoryEntity { get; set; } // dodato*/
    }
}
