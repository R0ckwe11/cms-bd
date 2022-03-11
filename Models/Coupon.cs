using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Coupons")]
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Name { get; set; }
        public int ImageID { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public int Order { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int IsVisible { get; set; }
        public int IsArchived { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }

        [ForeignKey(nameof(ImageID))]
        [InverseProperty("Coupons")]
        public virtual ImageMetadata ImageMetadataSet { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("CouponsUpdated")]
        public virtual User UserUpdating { get; set; }

        [InverseProperty(nameof(TagCouponPivot.CouponPivot))]
        public virtual ICollection<TagCouponPivot> TagCouponPivots { get; set; }
        [InverseProperty(nameof(UsedCoupon.CouponUsed))]
        public virtual ICollection<UsedCoupon> UsedBy { get; set; }
    }
}