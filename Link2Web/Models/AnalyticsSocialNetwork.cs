using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class AnalyticsSocialNetwork
    {
        public int AnalyticsSocialNetworkId { get; set; }
        public string Name { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        public bool Visible { get; set; }

        public ICollection<AnalyticsSocial> AnalyticsSocial { get; set; }
    }
}