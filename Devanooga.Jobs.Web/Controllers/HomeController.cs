namespace Devanooga.Jobs.Web.Controllers
{
    using System.Threading.Tasks;
    using Devanooga.Jobs.Data.Entity;
    using Devanooga.Jobs.Data.Entity.Models;
    using Devanooga.Jobs.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("")]
    [ApiVersion("0")]
    public class HomeController : Controller
    {
        protected JobContext JobContext { get; }

        public HomeController(JobContext jobContext)
        {
            JobContext = jobContext;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(await JobContext.Jobs.ToListAsync());
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Post([FromBody] HomeTestModel model)
        {
            var job = new Job
            {
                Description = model.A
            };
            JobContext.Jobs.Add(job);
            await JobContext.SaveChangesAsync();
            return Ok(job);
        }
    }
}