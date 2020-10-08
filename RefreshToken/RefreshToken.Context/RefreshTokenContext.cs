using Microsoft.EntityFrameworkCore;
using RefreshToken.Models.Entities;

namespace RefreshToken.Context
{
    public class RefreshTokenContext : DbContext
    {
        public RefreshTokenContext(DbContextOptions<RefreshTokenContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
