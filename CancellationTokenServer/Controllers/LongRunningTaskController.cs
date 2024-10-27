using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CancellationTokenServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LongRunningTaskController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            // Simulate a long-running task
            for (int i = 0; i < 10; i++)
            {
                // Check if cancellation is requested
                if (cancellationToken.IsCancellationRequested)
                {
                    return StatusCode(499, "Operation canceled.");
                }

                // Simulate work
                await Task.Delay(1000, cancellationToken);
            }

            return Ok("Task completed successfully.");
        }
    }
}
