using Microsoft.EntityFrameworkCore;
using MoviesMvcCoreBilgeAdam.Entities;

namespace MoviesMvcCoreBilgeAdam.Contexts
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Windows Authentication
            string connectionString = "server=.\\SQLEXPRESS;database=BA_MoviesCore;trusted_connection=true;";
            //string connectionString = "server=.;database=BA_MoviesCore;trusted_connection=true;";

            // SQL Server Authentication
            //string connectionString = "server=.\\SQLEXPRESS;database=BA_MoviesCore;user id=sa;password=sa;";
            //string connectionString = "server=.;database=BA_MoviesCore;user id=sa;password=123;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
