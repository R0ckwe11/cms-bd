using cms_bd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cms_bd.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        private void SeedUsers(ModelBuilder builder)  
        {  
            var user = new User()  
            {   
                Id = 1,
                UserName = "admin",
                Email = "admin@admin.com",
            };  
  
            var passwordHasher = new PasswordHasher<User>();  
            passwordHasher.HashPassword(user, "admin");  
  
            builder.Entity<User>().HasData(user);  
        }

        public IConfiguration Configuration { get; }

        public DbSet<Config> Config { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ImageMetadata> ImageMetadata { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCouponPivot> TagCouponPivot { get; set; }
        public DbSet<UsedCoupon> UsedCoupons { get; set; }
        // public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Config>()
                .HasOne(c => c.UserUpdating)
                .WithMany(u => u.ConfigsUpdated)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.ImageMetadataSet)
                .WithMany(i => i.Coupons)
                .HasForeignKey(c => c.ImageID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.UserUpdating)
                .WithMany(u => u.CouponsUpdated)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ImageMetadata>()
                .HasOne(i => i.UserCreating)
                .WithMany(u => u.ImagesCreated)
                .HasForeignKey(i => i.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.ImageMetadataSet)
                .WithMany(i => i.Posts)
                .HasForeignKey(p => p.ImageID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.UserUpdating)
                .WithMany(u => u.PostsUpdated)
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.UserUpdating)
                .WithMany(u => u.TagsUpdated)
                .HasForeignKey(t => t.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TagCouponPivot>()
                .HasOne(t => t.CouponPivot)
                .WithMany(c => c.TagCouponPivots)
                .HasForeignKey(t => t.CouponID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TagCouponPivot>()
                .HasOne(t => t.TagPivot)
                .WithMany(t => t.TagCouponPivots)
                .HasForeignKey(t => t.TagID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UsedCoupon>()
                .HasOne(u => u.CouponUsed)
                .WithMany(c => c.UsedBy)
                .HasForeignKey(u => u.CouponID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UsedCoupon>()
                .HasOne(u => u.UserUsing)
                .WithMany(u => u.UsedCoupons)
                .HasForeignKey(u => u.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Config>()
                .Property(c => c.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Coupon>()
                .Property(c => c.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ImageMetadata>()
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Post>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Tag>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<UsedCoupon>()
                .Property(u => u.UsedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            modelBuilder.Entity<User>()
                .Property(u => u.LastLogin)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            this.SeedUsers(modelBuilder);
        }
    }
}