using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Config")]
    public class Config
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string BackgroundColor { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("UserUpdating")]
        public int UpdatedBy { get; set; }

        public virtual User UserUpdating { get; set; }
    }
}