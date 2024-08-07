//using alahaly_momken.Entites;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using alahaly_momken.Entites;
//using YourNamespace;


//namespace alahaly_momken.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public ValuesController(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        // POST: api/values/deposit
//        [HttpPost("deposit")]
//        public async Task<IActionResult> CreateDeposit([FromBody] Depoist deposit)
//        {
//            try
//            {
//                // Add the deposit to the DbContext and save changes
//                _dbContext.Depoists.Add(deposit);
//                await _dbContext.SaveChangesAsync();

//                // Return the deposited object
//                return Ok(deposit);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating deposit: " + ex.Message);
//            }
//        }
//    }
//}


using alahaly_momken.Entites;
 // Make sure to include the correct namespace for your entities
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YourNamespace; // Make sure to include the correct namespace for your DbContext

namespace alahaly_momken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ValuesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/values/deposit
        [HttpPost("deposit")]
        public async Task<IActionResult> CreateDeposit([FromBody] Depoist deposit) // Corrected the parameter type
        {
            try
            {
                // Add the deposit to the DbContext and save changes
                _dbContext.Depoists.Add(deposit); // Corrected the DbSet name
                await _dbContext.SaveChangesAsync();

                // Return the deposited object
                return Ok(deposit);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error creating deposit: " + ex.Message);

                // Return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating deposit: An unexpected error occurred.");
            }
        }
    }
}
