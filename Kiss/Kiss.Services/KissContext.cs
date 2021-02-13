using Kiss.Models.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Services
{
    public class KissContext : DbContext
    {
        public KissContext(DbContextOptions<KissContext> options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
