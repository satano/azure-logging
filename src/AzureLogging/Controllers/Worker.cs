using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureLogging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Worker : ControllerBase
    {
        private static readonly Random _random = new();

        private readonly ILogger _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<string> Do(string name)
        {
            _logger.LogInformation($"Started work on task {name}.");

            Stopwatch sw = Stopwatch.StartNew();

            int workAmount = _random.Next(3000);
            if (workAmount > 2000)
            {
                _logger.LogError($"Work on task {name} is too expensive: {workAmount} ms");
                throw new BadHttpRequestException($"Work on task {name} is too expensive: {workAmount} ms");
            }

            _logger.LogDebug($"Working on task {name}, job 1.");
            await Task.Delay(workAmount / 2);

            _logger.LogDebug($"Working on task {name}, job 2.");
            await Task.Delay(workAmount / 2);

            sw.Stop();

            _logger.LogInformation($"Finished work on task {name} in {sw.ElapsedMilliseconds} ms.");

            return $"Worked on task {name} for {sw.ElapsedMilliseconds} ms.";
        }
    }
}
