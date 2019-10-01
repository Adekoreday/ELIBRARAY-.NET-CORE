using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elibrary.Models
{
    public class Books
    {
        public int Id { get; set; }
         [Required]
         [StringLength(150, MinimumLength=2, ErrorMessage="Title of book must be valid")]
        public string Title {get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        [StringLength(50, MinimumLength=5, ErrorMessage="Author name must be valid")]
        public string Author {get; set; }
        public string description {get; set; }
        [Required]
        public bool available  {get; set; }

        
    }
}
