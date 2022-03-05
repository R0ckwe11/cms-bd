using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Coupons")]
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Photo { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int IsVisible { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public virtual User UserCreating { get; set; }
        public virtual User UserUpdating { get; set; }

        public virtual ICollection<TagCouponPivot> TagCouponPivots { get; set; }
        public virtual ICollection<UsedCoupon> UsedBy { get; set; }
    }
}