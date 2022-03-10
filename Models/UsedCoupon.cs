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
        public int CouponID { get; set; }
        public int UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UsedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(CouponID))]
        [InverseProperty("UsedBy")]
        public virtual Coupon CouponUsed { get; set; }
        [ForeignKey(nameof(UserID))]
        [InverseProperty("UsedCoupons")]
        public virtual User UserUsing { get; set; }
    }
}