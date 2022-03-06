using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("UsedCoupons")]
    public class UsedCoupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("UserUsing")]
        public int UserID { get; set; }
        [ForeignKey("CouponUsed")]
        public int CouponID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UsedAt { get; set; } = DateTime.Now;

        public virtual User UserUsing { get; set; }
        public virtual Coupon CouponUsed { get; set; }
    }
}