using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("UsedCoupons")]
    public class UsedCoupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserUsing")]
        public int UserId { get; set; }
        [ForeignKey("CouponUsed")]
        public int CouponId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UsedAt { get; set; } = DateTime.Now;

        public virtual User UserUsing { get; set; }
        public virtual Coupon CouponUsed { get; set; }
    }
}