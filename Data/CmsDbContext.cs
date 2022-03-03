using cms_bd.Models;
using Microsoft.EntityFrameworkCore;

namespace cms_bd.Data
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCouponPivot> TagCouponPivot { get; set; }
        public DbSet<UsedCoupon> UsedCoupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Config>().ToTable("Config");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Coupon>().ToTable("Coupon");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<TagCouponPivot>().ToTable("TagCouponPivot");
            modelBuilder.Entity<UsedCoupon>().ToTable("UsedCoupon");
        }
    }
}