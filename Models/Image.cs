using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FileName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("ImagesCreated")]
        public virtual User UserCreating { get; set; }

        [InverseProperty(nameof(Coupon.ImageSet))]
        public virtual ICollection<Coupon> Coupons { get; set; }
        [InverseProperty(nameof(Post.ImageSet))]
        public virtual ICollection<Post> Posts { get; set; }
    }
}