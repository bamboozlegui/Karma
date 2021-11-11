using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Karma.Services
{
    public class KarmaPointService
    {

        private readonly ILogger<KarmaPointService> _logger;

        public KarmaPointService(ILogger<KarmaPointService> logger)
        {
            _logger = logger;
        }

        public void OnItemPosted(object source, EventArgs args)
        {
            _logger.LogInformation("Item posted!");
        }
    }
}
