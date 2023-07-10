using System.Text.Json.Serialization;

namespace ApiContracts.Messages
{
    public class NotificationMessage : IMessage
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("notification")]
        public string Notification { get; set; } = string.Empty;
        [JsonIgnore]
        public string MessageTypeName => nameof(NotificationMessage);
    }
}
