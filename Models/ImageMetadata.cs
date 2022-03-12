using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("ImageMetadata")]
    public class ImageMetadata
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

        [InverseProperty(nameof(Coupon.ImageMetadataSet))]
        public virtual ICollection<Coupon> Coupons { get; set; }
        [InverseProperty(nameof(Post.ImageMetadataSet))]
        public virtual ICollection<Post> Posts { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}