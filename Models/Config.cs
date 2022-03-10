using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_bd.Models
{
    [Table("Config")]
    public class Config
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("ConfigsUpdated")]
        public virtual User UserUpdating { get; set; }
    }
}