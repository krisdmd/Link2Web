using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class Link
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LinkId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("LinkStatus")]
        public int StatusId { get; set; }

        [ForeignKey("LinkTypes")]
        public int LinkTypeId { get; set; }

        public int ContactId { get; set; }

        [ForeignKey("Projects")]
        public int ProjectId { get; set; }

        public string WebsiteUrl { get; set; }

        public string AnchorText { get; set; }

        public string DestinationUrl { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        [DefaultValue(false)]
        public bool BacklinkFound { get; set; }

    }
}