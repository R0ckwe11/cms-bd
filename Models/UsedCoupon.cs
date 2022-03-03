using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    public class UsedCoupon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CouponId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UsedAt { get; set; }
    }
}