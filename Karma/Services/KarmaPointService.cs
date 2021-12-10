﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Microsoft.Extensions.Logging;
using Karma.Areas.Identity.Data;

namespace Karma.Services
{
    public class KarmaPointService
    {

        private readonly ILogger<KarmaPointService> _logger;
        public KarmaDbContext KarmaDbContext { get; }

        public KarmaPointService(ILogger<KarmaPointService> logger, KarmaDbContext karmaDbContext)
        {
            _logger = logger;
            KarmaDbContext = karmaDbContext;
        }

        public void OnItemPosted(object source, EventArgs args) 
        {
            _logger.LogInformation("Item posted!");
        }
        
        public async Task ProcessKarmaBalanceAsync(string id, Func<int, int> processKP)
        {
            var user = await KarmaDbContext.Users.FindAsync(id);
            _logger.LogInformation("{0}", user.KarmaPoints);
            user.KarmaPoints = processKP(user.KarmaPoints);
            await KarmaDbContext.SaveChangesAsync();
        }

        public void ProcessKarmaBalance(string id, Func<int, int> processKP)
        {
            var user = KarmaDbContext.Users.Find(id);
            _logger.LogInformation("{0}", user.KarmaPoints);
            user.KarmaPoints = processKP(user.KarmaPoints);
            KarmaDbContext.SaveChanges();
        }
    }
}
