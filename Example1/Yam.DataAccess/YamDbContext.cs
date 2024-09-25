using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yam.Model.Models;

namespace Yam.DataAccess
{
    public class YamDbContext : IdentityDbContext
    {
        public YamDbContext(DbContextOptions<YamDbContext> opt) : base(opt)
        {
        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
