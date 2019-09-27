namespace Elibrary.Models
{
    public class Books
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Author {get; set; }
        public string description {get; set; }
        public bool available  {get; set; }
        
    }
}
