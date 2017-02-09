using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class SettingType
    {
        public int SettingTypeId { get; set; }
        public string Type { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        public Boolean Visible { get; set; }

        public ICollection<Setting> Settings { get; set; }
    }
}