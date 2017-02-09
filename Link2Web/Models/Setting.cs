using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class Setting
    {
        public int SettingId { get; set; }
        [Display(Name = "Setting")]
        public int SettingTypeId { get; set; }
        public ApplicationUser UserId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        public Boolean Visible { get; set; }

        public virtual SettingType SettingType { get; set; }
    }
}
