using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    public class Config
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string BackgroundColor { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
    }
}