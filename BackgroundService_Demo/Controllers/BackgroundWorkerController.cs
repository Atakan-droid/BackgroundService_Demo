using BackgroundService_Demo.BackgroundService;
using BackgroundService_Demo.Entities;
using BackgroundService_Demo.Queues;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundService_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundWorkerController : ControllerBase
    {
        private readonly IBackgroundTaskQueue<Entity> _queue;
        private IQueueHostedService _queueHostedService;

        public BackgroundWorkerController(IBackgroundTaskQueue<Entity> queue, IQueueHostedService queueHostedService)
        {
            _queue = queue;
            _queueHostedService = queueHostedService;
        }

        [HttpPost]
        public IActionResult AddQueueAsync(Entity[] entities)
        {
            foreach (var entity in entities)
            {
                _queue.AddQueueAsync(entity);
            }

            return Ok();
        }

        [HttpGet("stop")]
        public IActionResult StopService()
        {
            _queueHostedService.StopAsync();
            return Ok();
        }
        [HttpGet("start")]
        public IActionResult StartService()
        {
            _queueHostedService.StartAsync();
            return Ok();
        }
    }
}
