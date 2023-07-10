using Amazon.SQS;
using ApiContracts.Messages;
using Microsoft.AspNetCore.Mvc;
using SqsSender.Services;

namespace SqsSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(ILogger<NotificationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string msg)
        {
            var sqsClient = new AmazonSQSClient(region: Amazon.RegionEndpoint.USWest2);

            var publisher = new SqsPublisher(sqsClient);

            await publisher.PublishAsync<NotificationMessage>("mercedes-notification", 
                new NotificationMessage
            {
                    Id = Guid.NewGuid(),
                    Notification = msg,
                    Type = 1
            });

            return Ok();
        }
    }
}
