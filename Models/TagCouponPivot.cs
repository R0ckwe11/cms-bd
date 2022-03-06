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
        [ForeignKey("CouponPivot")]
        public int CouponID { get; set; }
        [ForeignKey("TagPivot")]
        public int TagID { get; set; }

        public virtual Coupon CouponPivot { get; set; }
        public virtual Tag TagPivot { get; set; }
    }
}