using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("TagCouponPivot")]
    public class TagCouponPivot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CouponID { get; set; }
        public int TagID { get; set; }

        [ForeignKey(nameof(CouponID))]
        [InverseProperty("TagCouponPivots")]
        public virtual Coupon CouponPivot { get; set; }
        [ForeignKey(nameof(TagID))]
        [InverseProperty("TagCouponPivots")]
        public virtual Tag TagPivot { get; set; }
    }
}