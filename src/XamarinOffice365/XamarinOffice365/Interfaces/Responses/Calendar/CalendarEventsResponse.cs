using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;

namespace XamarinOffice365.Interfaces.Responses.Calendar
{
    public class CalendarEventsResponse
    {    
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<CalendarEvent> Events { get; set; }
    }

    public class CalendarEvent
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "Start")]
        public DateTime Start { get; set; }

        [JsonProperty(PropertyName = "End")]
        public DateTime End { get; set; }

        [JsonProperty(PropertyName = "Location")]
        public CalendarEventLocation Location { get; set; }

        public string ShortTimeSpan
        {
            get { return string.Format("{0} - {1}", Start.ToLocalTime().ToString("t"), End.ToLocalTime().ToString("t")); }
        }

        public string ShortDate
        {
            get { return Start.ToLocalTime().ToString("D"); }
        }
    }

    public class CalendarEventLocation
    {
        [JsonProperty(PropertyName = "DisplayName")]
        public string LocationName { get; set; }
    }
}