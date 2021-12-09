using Karma.Data;
using Karma.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmaService.Tests
{
    [TestClass]
    class KaraPointServiceTests
    {
        private readonly ILogger<KarmaPointService> _logger;
        public KarmaDbContext KarmaDbContext { get; }

        public KarmaPointService KarmaPointService { get; }

        Func<int, int> Addd = (x => x + 6);
        Func<int, int> Subtract = (x => x - 6);

        public KaraPointServiceTests(ILogger<KarmaPointService> logger, KarmaDbContext karmaDbContext)
        {
            _logger = logger;
            KarmaDbContext = karmaDbContext;
        }

        

        [TestMethod]
        public async Task ProcessKarmaBalance_ReturnsBalance()
        {
            string id = "3c3e4853-7deb-4959-95a2-f7efc428cb2d";

           var kazkas = await KaraPointService.
        }
    }
}
