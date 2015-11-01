using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinOffice365.Interfaces;
using XamarinOffice365.Interfaces.Responses.Messages;

[assembly: Dependency(typeof(XamarinOffice365.Services.Office365ExchangeService))]

namespace XamarinOffice365.Services
{
    public class Office365ExchangeService : IOffice365ExchangeService
    {
        private const string GetMessagesUrl = "https://graph.microsoft.com/beta/me/messages";
        
        public List<Message> GetMessages(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) {
                throw new InvalidOperationException("Access token missing");
            }
            
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(GetMessagesUrl)))
                {
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                    using (var response = client.SendAsync(request).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;
                            var messagesResponse = JsonConvert.DeserializeObject<MessagesResponse>(json);
                            return messagesResponse.Messages;
                        }
                    }
                }
            }

            throw new Exception("Could not get messages");
        }
    }
}