using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class LinkType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkTypeId { get; set; }

        public string Type { get; set; }

        [DefaultValue(true)]
        public bool Visible { get; set; }
    }
}