using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class LinkStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkStatusId { get; set; }

        public string Status { get; set; }


        [DefaultValue(true)]
        public bool Visible { get; set; }
    }
}