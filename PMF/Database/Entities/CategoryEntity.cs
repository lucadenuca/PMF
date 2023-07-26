using System.ComponentModel.DataAnnotations;

namespace PMF.Database.Entities
{
    public class CategoryEntity
    {
        [Key]
        public string Code { get; set; }
        public string ParentCode { get; set; } // If a category has a parent category
        public string Name { get; set; }
    }
}
