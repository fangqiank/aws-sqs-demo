using Amazon.SQS;
using Amazon.SQS.Model;

namespace SqsReceiver.Services
{
    public class SqsService: BackgroundService
    {
        private readonly IAmazonSQS _sqs;

        public SqsService(IAmazonSQS sqs)
        {
            _sqs = sqs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueUrl = await _sqs.GetQueueUrlAsync("mercesdes-notification");

            var receiveReq = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl.QueueUrl,
                MessageAttributeNames = new List<string> { "All" },
                AttributeNames = new List<string> { "All" },
            };

            while (!stoppingToken.IsCancellationRequested) 
            {
                var receivedRes = await _sqs.ReceiveMessageAsync(receiveReq, stoppingToken);

                if(receivedRes.HttpStatusCode != System.Net.HttpStatusCode.OK) 
                {
                    Console.WriteLine(receivedRes.HttpStatusCode);
                    continue;
                }

                foreach (var msg in receivedRes.Messages)
                {
                    Console.WriteLine(msg.Body);
                    await _sqs.DeleteMessageAsync(queueUrl.QueueUrl, msg.ReceiptHandle);
                }
            }
        }
    }
}
