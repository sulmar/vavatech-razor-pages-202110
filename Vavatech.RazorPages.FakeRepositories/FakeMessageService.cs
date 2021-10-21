using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Vavatech.RazorPages.IRepositories;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeMessageService : IMessageService
    {
        private readonly ILogger<FakeMessageService> logger;

        public FakeMessageService(ILogger<FakeMessageService> logger)
        {
            this.logger = logger;
        }

        public void Send(string message)
        {
            logger.LogInformation(message);
        }
    }
}
