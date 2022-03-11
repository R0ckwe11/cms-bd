using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public int ImageID { get; set; }
        public string? Content { get; set; }
        public int IsVisible { get; set; }
        public int IsInMenu { get; set; }
        public int Order { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }

        [ForeignKey(nameof(ImageID))]
        [InverseProperty("Posts")]
        public virtual ImageMetadata ImageMetadataSet { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("PostsUpdated")]
        public virtual User UserUpdating { get; set; }
    }
}