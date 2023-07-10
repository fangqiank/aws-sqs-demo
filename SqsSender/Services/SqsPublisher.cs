using Amazon.SQS;
using Amazon.SQS.Model;
using ApiContracts.Messages;
using System.Text.Json;

namespace SqsSender.Services
{
    public class SqsPublisher
    {
        private readonly IAmazonSQS _amazonSQS;

        public SqsPublisher(IAmazonSQS amazonSQS)
        {
            _amazonSQS = amazonSQS;
        }

        public async Task PublishAsync<T>(string queueName, T message) where T : IMessage
        {
            var queueUrl = await _amazonSQS.GetQueueUrlAsync(queueName);

            var req = new SendMessageRequest
            {
                QueueUrl = queueUrl.QueueUrl,
                MessageBody = JsonSerializer.Serialize(message),
                MessageAttributes = new Dictionary<string, MessageAttributeValue> 
                {
                    {
                        nameof(IMessage.MessageTypeName),
                        new MessageAttributeValue
                        {
                            StringValue = message.MessageTypeName,
                            DataType = "String"
                        }
                    },

                    {
                        "timestamp",
                        new MessageAttributeValue
                        {
                            StringValue = DateTime.UtcNow.ToString(),
                            DataType = "String"
                        }
                    },

                    {
                        "version",
                        new MessageAttributeValue
                        {
                            StringValue = "v1",
                            DataType = "String"
                        }
                    }
                }
            };

            await _amazonSQS.SendMessageAsync(req);
        }
    }
}
