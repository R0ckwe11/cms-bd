using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Name { get; set; }
        public int IsArchived { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("TagsUpdated")]
        public virtual User UserUpdating { get; set; }

        [InverseProperty(nameof(TagCouponPivot.TagPivot))]
        public virtual ICollection<TagCouponPivot> TagCouponPivots { get; set; }
    }
}