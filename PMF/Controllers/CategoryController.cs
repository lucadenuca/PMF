using Microsoft.AspNetCore.Mvc;
using PMF.Database;
using PMF.Database.Entities;
using PMF.Services;

namespace PMF.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesImporter _csvImporter;
        private readonly ApplicationDbContext _dbContext;

        public CategoriesController(CategoriesImporter csvImporter, ApplicationDbContext dbContext)
        {
            _csvImporter = csvImporter;
            _dbContext = dbContext;
        }

        [HttpPost("import")]
        public IActionResult ImportCategories()
        {
            
            List<CategoryEntity> records = _csvImporter.ImportCategories();
            try
            {
                _dbContext.Categories.AddRange(records);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving the data: " + ex.Message);
            }


            return Ok("Categories imported successfully.");
        }
    }
}
