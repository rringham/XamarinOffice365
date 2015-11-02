using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamarinOffice365.Interfaces.Responses.Messages
{
    public class MessagesResponse
    {    
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "Body")]
        public MessageBody Body { get; set; }

        [JsonProperty(PropertyName = "Sender")]
        public Sender Sender { get; set; }

        [JsonProperty(PropertyName = "From")]
        public From From { get; set; }

        [JsonProperty(PropertyName = "IsRead")]
        public bool IsRead { get; set; }

        public string UnreadLabel
        {
            get
            {
                return IsRead ? "" : "•";
            }
        }
    }

    public class MessageBody
    {
        [JsonProperty(PropertyName = "ContentType")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "Content")]
        public string Content { get; set; }
    }

    public class Sender
    {
        [JsonProperty(PropertyName = "EmailAddress")]
        public EmailAddress EmailAddress { get; set; }
    }

    public class From
    {
        [JsonProperty(PropertyName = "EmailAddress")]
        public EmailAddress EmailAddress { get; set; }

        public string FromDescription
        {
            get
            {
                return string.Format("{0} <{1}>", EmailAddress?.Name, EmailAddress?.Address);
            }
        }
    }

    public class EmailAddress
    {
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
    }
}