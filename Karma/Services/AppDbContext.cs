using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Karma.Models;

namespace Karma.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {      
        }
        public DbSet<ItemPost> Items { get; set; }
        public DbSet<RequestPost> Requests { get; set; }
    }
}
