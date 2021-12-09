using Karma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karma.Services
{
    public interface IFulfillmentRepository
    {
        public Task<List<Fulfillment>> GetFulfillmentsAsync();

        public Task<Fulfillment> GetFulfillmentAsync(int id);
        public Task<Fulfillment> AddFulfillmentAsync(int requestId, int fulfillerId);
        public Fulfillment AddFulfillment(int requestId, int fulfillerId);

        public Task<Fulfillment> DeleteFulfillmentAsync(int id);

        public Task<Fulfillment> UpdateFulfillmentAsync(Fulfillment newFulfillment);
    }
}
