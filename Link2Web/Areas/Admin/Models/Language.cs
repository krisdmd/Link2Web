﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Areas.Admin.Models
{
    public class Language
    {
        public int LanguageId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Code { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Default", ResourceType = typeof(Resources.Resources))]
        public bool Default { get; set; }
    }
}