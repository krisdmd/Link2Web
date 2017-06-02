﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class Email
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MailId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("Projects")]
        public int ProjectId { get; set; }

        public Models.Project Project { get; set; }

        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public DateTime Created { get; set; }
    }
}