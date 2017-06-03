using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Areas.Admin.Models
{
    public class SettingType
    {
        public int SettingTypeId { get; set; }
        [Required]
        public string Type { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        public Boolean Visible { get; set; }
    }
}