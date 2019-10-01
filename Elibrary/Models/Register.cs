  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Elibrary.Models
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength=6, ErrorMessage="password should not be less than 6 characters long")]
        public string Password { get; set; }

        [Required]
         [StringLength(5, MinimumLength=4, ErrorMessage="Enter a valid type Admin or User")]
        public string Type { get; set; }
    }

}