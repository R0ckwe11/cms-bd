using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }

        public virtual ICollection<Config> ConfigsUpdated { get; set; }
        public virtual ICollection<Coupon> CouponsCreated { get; set; }
        public virtual ICollection<Coupon> CouponsUpdated { get; set; }
        public virtual ICollection<Post> PostsCreated { get; set; }
        public virtual ICollection<Post> PostsUpdated { get; set; }
        public virtual ICollection<Tag> TagsCreated { get; set; }
        public virtual ICollection<Tag> TagsUpdated { get; set; }
        public virtual ICollection<UsedCoupon> UsedCoupons { get; set; }
    }
}