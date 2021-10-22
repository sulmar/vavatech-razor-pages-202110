using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.RazorPages.Models;

namespace WebApp.Hubs
{
    public class CustomersHub : Hub
    {
        private readonly ILogger<CustomersHub> logger;

        public CustomersHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            logger.LogInformation("Connected {0}", this.Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task SendAddedCustomer(Customer customer)
        {
            await Clients.Others.SendAsync("AddedCustomer", customer);
        }

        public async Task Ping()
        {
            await Clients.Caller.SendAsync("Pong");
        }
    }
}
