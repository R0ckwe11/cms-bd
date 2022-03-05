using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("TagCouponPivot")]
    public class TagCouponPivot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CouponPivot")]
        public int CouponId { get; set; }
        [ForeignKey("TagPivot")]
        public int TagId { get; set; }

        public virtual Coupon CouponPivot { get; set; }
        public virtual Tag TagPivot { get; set; }
    }
}