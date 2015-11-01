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
    }
}