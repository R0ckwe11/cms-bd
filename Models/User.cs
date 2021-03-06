using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace cms_bd.Models
{
    [Table("Users")]
    public class User : IdentityUser<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }

        [InverseProperty(nameof(Config.UserUpdating))]
        public virtual ICollection<Config> ConfigsUpdated { get; set; }
        [InverseProperty(nameof(Coupon.UserUpdating))]
        public virtual ICollection<Coupon> CouponsUpdated { get; set; }
        [InverseProperty(nameof(ImageMetadata.UserCreating))]
        public virtual ICollection<ImageMetadata> ImagesCreated { get; set; }
        [InverseProperty(nameof(Post.UserUpdating))]
        public virtual ICollection<Post> PostsUpdated { get; set; }
        [InverseProperty(nameof(Tag.UserUpdating))]
        public virtual ICollection<Tag> TagsUpdated { get; set; }
        [InverseProperty(nameof(UsedCoupon.UserUsing))]
        public virtual ICollection<UsedCoupon> UsedCoupons { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}