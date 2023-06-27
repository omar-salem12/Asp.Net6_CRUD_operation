using Microsoft.EntityFrameworkCore;
using MovieStore.Models;

namespace MovieStore.Data
{
    public class MovieDbContext :DbContext
    {

        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        { 
        }


        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
