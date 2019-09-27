using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Elibrary.Models
{
    public class BooksDbContext : DbContext {
        
        public DbSet<Books> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("server=ADE\\ELIBRARY; database=BooksDB; Trusted_Connection=true;");
        }
}