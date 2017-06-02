using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }
        [ForeignKey("SettingTypes")]
        public int SettingTypeId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int ValueInt { get; set; }

        [DefaultValue(true)]
        public bool Visible { get; set; }
    }
}