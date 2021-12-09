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
        public List<Fulfillment> GetFulfillments();

        public Task<Fulfillment> GetFulfillmentAsync(int id);
        public Task<Fulfillment> AddFulfillmentAsync(int requestId, string fulfillerId);
        public Fulfillment GetFulfillmentByRequestId(int id);
        public Fulfillment AddFulfillment(int requestId, string fulfillerId);

        public Task<Fulfillment> DeleteFulfillmentAsync(int id);

        public Task<Fulfillment> UpdateFulfillmentAsync(Fulfillment newFulfillment);
    }
}
