using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinOffice365.Interfaces;
using XamarinOffice365.Interfaces.Responses.Calendar;
using XamarinOffice365.Interfaces.Responses.Messages;

[assembly: Dependency(typeof(XamarinOffice365.Services.Office365ExchangeService))]

namespace XamarinOffice365.Services
{
    public class Office365ExchangeService : IOffice365ExchangeService
    {
        private const string GetMessagesUrl = "https://outlook.office.com/api/v1.0/me/messages";
        private const string GetCalendarEventsUrl = "https://outlook.office.com/api/v1.0/me/events";
        
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

            return new List<Message>();
        }

        public List<CalendarEvent> GetCalendarEvents(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) {
                throw new InvalidOperationException("Access token missing");
            }

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(GetCalendarEventsUrl)))
                {
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                    using (var response = client.SendAsync(request).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;
                            var calendarEventsResponse = JsonConvert.DeserializeObject<CalendarEventsResponse>(json);
                            var sortedEvents = (from calendarEvent in calendarEventsResponse.Events
                                orderby calendarEvent.Start ascending
                                select calendarEvent).ToList();
                            return sortedEvents;
                        }
                    }
                }
            }

            return new List<CalendarEvent>();
        }
    }
}