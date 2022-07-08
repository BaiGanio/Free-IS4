using Microsoft.EntityFrameworkCore;

namespace IS4
{
    public class SQLDbContext : DbContext
    {
        private readonly string _conn;

        public SQLDbContext(string dbconn) { _conn = dbconn; }
        public SQLDbContext(DbContextOptions<SQLDbContext> options)
           : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region UserRoles

            builder
                .Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });

            builder
                .Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);

            builder
                .Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);

            #endregion

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_conn))
            {
                optionsBuilder.UseSqlServer(_conn);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
