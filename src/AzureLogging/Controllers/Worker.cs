using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureLogging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Worker : ControllerBase
    {
        private static readonly Random _random = new();

        [HttpGet("{name}")]
        public async Task<string> Do(string name)
        {
            Stopwatch sw = Stopwatch.StartNew();

            await Task.Delay(_random.Next(2000));
            sw.Stop();

            return $"Worked on task {name} for {sw.ElapsedMilliseconds} ms.";
        }
    }
}
