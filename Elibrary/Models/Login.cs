using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elibrary.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength=4, ErrorMessage="password should not be less than 4 characters long")]
        public string Password { get; set; }
    }
}