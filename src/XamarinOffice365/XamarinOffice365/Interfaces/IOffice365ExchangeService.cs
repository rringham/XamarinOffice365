﻿using System.Collections.Generic;
using XamarinOffice365.Interfaces.Responses.Messages;

namespace XamarinOffice365.Interfaces
{
    public interface IOffice365ExchangeService
    {
        List<Message> GetMessages(string accessToken);
    }
}