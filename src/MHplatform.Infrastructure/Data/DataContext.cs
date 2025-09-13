using MHPlatform.Domain.Entities;
using MHPlatform.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHplatform.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<OrderForm> OrderForm { get; set; }

        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserClaim> UserClaim { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
