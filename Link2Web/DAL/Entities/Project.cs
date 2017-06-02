using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        [ForeignKey("Language")]
        public int LanguageId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [MaxLength(12)]
        public string ViewProfileId { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }

        public string Url { get; set; }

        [MaxLength(255)]
        public string PreviewImage { get; set; }

        public string Note { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        [DefaultValue(true)]
        public bool Visible { get; set; }

    }
}