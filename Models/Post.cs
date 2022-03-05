using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Content { get; set; }
        public int IsVisible { get; set; }
        public int Order { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public virtual User UserCreating { get; set; }
        public virtual User UserUpdating { get; set; }
    }
}