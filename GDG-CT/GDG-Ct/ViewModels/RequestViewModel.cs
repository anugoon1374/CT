using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Beyond.Ct.Web.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Context { get; set; }

        [Required]
        [EmailAddress]
        public List<string> ReceiverEmail { get; set; } = new List<string>();

        [Required]
        public List<string> ReceiverName { get; set; } = new List<string>();

        [Required]
        [EmailAddress]
        public string RequesterEmail { get; set; }
    }
}