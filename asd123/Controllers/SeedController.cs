using asd123.Services;
using asd123.UseCase.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeedController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[HttpPost]
        //[Route("seed-database")]
        //public async Task<IActionResult> SeedDatabase()
        //{
        //    var seeder = new DatabaseSeeder(_unitOfWork);
        //    var response = await seeder.Seed();

        //    if (response.Status == "success")
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return StatusCode(500, response);
        //    }
        //}
    }
}
