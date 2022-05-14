using Microsoft.AspNetCore.SignalR.Client;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageConsumer.Services.Abstract
{
    public interface IHubClientBuilder : IHubMessageDispatcher
    {
        HubConnection GetHubConnection();
    }
}
