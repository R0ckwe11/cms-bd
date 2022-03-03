using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    public class TagCouponPivot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int TagId { get; set; }
    }
}