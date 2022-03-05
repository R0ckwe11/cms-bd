using cms_bd.Models;
using Microsoft.EntityFrameworkCore;

namespace cms_bd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Config> Config { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCouponPivot> TagCouponPivot { get; set; }
        public DbSet<UsedCoupon> UsedCoupons { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.UserCreating)
                .WithMany(u => u.CouponsCreated)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.UserUpdating)
                .WithMany(u => u.CouponsUpdated)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Post>()
                .HasOne(p => p.UserCreating)
                .WithMany(u => u.PostsCreated)
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.UserUpdating)
                .WithMany(u => u.PostsUpdated)
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Tag>()
                .HasOne(t => t.UserCreating)
                .WithMany(u => u.TagsCreated)
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.UserUpdating)
                .WithMany(u => u.TagsUpdated)
                .HasForeignKey(t => t.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}