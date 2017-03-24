using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        [Required]
        [Display(Name = "Code", ResourceType = typeof(Resources.Resources))]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Symbol", ResourceType = typeof(Resources.Resources))]
        public string Symbol { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}