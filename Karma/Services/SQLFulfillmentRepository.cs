using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Microsoft.EntityFrameworkCore;

namespace Karma.Services
{
    public class SQLFulfillmentRepository : IFulfillmentRepository
    {

        public SQLFulfillmentRepository(KarmaDbContext context)
        {
            Context = context;
        }

        public KarmaDbContext Context { get; }


        public async Task<List<Fulfillment>> GetFulfillmentsAsync()
        {
            return await Context.Fulfillments.ToListAsync();
        }

        public async Task<Fulfillment> GetFulfillmentAsync(int id)
        {
            var fulfillment = await Context.Fulfillments.FirstOrDefaultAsync(i => i.Id == id);
            return fulfillment;
        }

        public async Task<Fulfillment> AddFulfillmentAsync(int requestId, int fulfillerId)
        {

            var request = await Context.Requests.FindAsync(requestId);
            var fulfiller = await Context.Users.FindAsync(fulfillerId);

            if (request == null || fulfiller == null) return null;

            var fulfillment = new Fulfillment
            {
                Fulfiller = fulfiller,
                Request = request,
                State = Fulfillment.FulfillmentEnum.InProgress
            };

            await Context.Fulfillments.AddAsync(fulfillment);
            await Context.SaveChangesAsync();
            return fulfillment;
        }

        public Fulfillment AddFulfillment(int requestId, int fulfillerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Fulfillment> DeleteFulfillmentAsync(int id)
        {
            Fulfillment fulfillment = await Context.Fulfillments.FindAsync(id);
            if(fulfillment != null)
            {
                Context.Fulfillments.Remove(fulfillment);
                await Context.SaveChangesAsync();
            }
                return fulfillment;
        }


        public Task<Fulfillment> UpdateFulfillmentAsync(Fulfillment newFulfillment)
        {
            throw new NotImplementedException();
        }

    }
}