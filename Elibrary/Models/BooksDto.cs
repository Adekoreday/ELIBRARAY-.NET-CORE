
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elibrary.Models
{
    public class BooksDto
    {
        public int Id { get; set; }
        public string Title {get; set; }
        public int ISBN { get; set; }
        public string Author {get; set; }
        public string description {get; set; }
        public bool available  {get; set; }
    }
}
