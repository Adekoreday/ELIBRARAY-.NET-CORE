using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Elibrary.Models
{
    public class BooksDbContext : DbContext {
        private readonly IConfiguration _config;

        public BooksDbContext(IConfiguration config, DbContextOptions<BooksDbContext> options) : base(options)
        {
            _config = config;
        }
        
        public DbSet<Books> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
        }
}