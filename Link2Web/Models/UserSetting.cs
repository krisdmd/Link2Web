using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class UserSetting
    {
        public int UserSettingId { get; set; }
        public string UserId { get; set; }
        public string Setting { get; set; }
        public string Value { get; set; }
        public int ValueInt { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}