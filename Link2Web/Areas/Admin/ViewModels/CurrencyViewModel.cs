﻿using Link2Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Areas.Admin.ViewModels
{
    public class CurrencyViewModel
    {
        public int CurrencyId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "Code", ResourceType = typeof(Resources.Resources))]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "Symbol", ResourceType = typeof(Resources.Resources))]
        public string Symbol { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public List<CurrencyViewModel> Currencies { get; set; }
    }
}