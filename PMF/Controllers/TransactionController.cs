using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PMF.Database;
using PMF.Database.Configuration;
using PMF.Database.Entities;
using PMF.Services;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PMF.Controllers
{
    [ApiController]
    [Route("v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ApplicationDbContext _dbContext;


        public TransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetTransactions([FromQuery] List<string> transactionKinds = null, [FromQuery] DateTime? startDate = null,
           [FromQuery] DateTime? endDate = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
           [FromQuery] string sortBy = "Date", [FromQuery] string sortOrder = "asc")
        {
            // Apply filtering based on query parameters
            IQueryable<TransactionEntity> query = _dbContext.Transactions;

            // Apply transactionKind filter
            if (transactionKinds != null && transactionKinds.Any())
            {
                query = query.Where(t => transactionKinds.Contains(t.Kind));
            }

            // Apply period filter
            if (startDate != null && endDate != null)
            {
                DateTime start = startDate.Value.Date;
                DateTime end = endDate.Value.Date.AddDays(1); // Include transactions up to the end of the selected day
                query = query.Where(t => t.Date >= start && t.Date < end);
            }

            // Calculate total number of records matching the filter conditions
            int totalCount = query.Count();

            // Apply sorting based on query parameters
            switch (sortBy.ToLower())
            {
                case "beneficiaryname":
                    query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.BeneficiaryName) : query.OrderBy(t => t.BeneficiaryName);
                    break;
                case "amount":
                    query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.Amount) : query.OrderBy(t => t.Amount);
                    break;
                // Add more cases for other sortable properties if needed
                default:
                    query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.Date) : query.OrderBy(t => t.Date);
                    break;
            }

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<TransactionEntity> transactions = query.ToList();

            // Return paginated result along with the total count
            return Ok(new { TotalCount = totalCount, Transactions = transactions });
        }

        [HttpPost("import")]
        public IActionResult ImportTransactions()
        {
            ImportCsv csvImporter = new ImportCsv();
            List<TransactionEntity> records = csvImporter.Import();

            try
            {
                _dbContext.Transactions.AddRange(records);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "An error occurred while saving the data: " + ex.Message);
            }

            return Ok("Data imported successfully!");
        }
    
        [HttpPost("{id}/split")]
        public IActionResult SplitTransaction()
        {
            return Ok();
        }

        [HttpPost("{id}/categorize")]
        public IActionResult CategorizeTransatcion()
        {
            return Ok();
        }

        [HttpPost("auto-categorize")]
        public IActionResult AutoCategorizeTransatcion()
        {
            return Ok();
        }

    }
    }
