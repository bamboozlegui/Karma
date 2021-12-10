﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Areas.Identity.Data;
using Karma.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karma.Data
{
    public class KarmaDbContext : IdentityDbContext<KarmaUser>
    {
        public KarmaDbContext(DbContextOptions<KarmaDbContext> options)
            : base(options)
        {
        }
        public DbSet<ItemPost> Items { get; set; }
        public DbSet<RequestPost> Requests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Fulfillment> Fulfillments { get; set; }
        public DbSet<Giveaway> Giveaways { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
