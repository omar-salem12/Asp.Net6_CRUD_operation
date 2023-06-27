using Microsoft.EntityFrameworkCore;
using MovieStore.Data;

namespace MovieStore.Models.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext context)
        {
            _context = context;
        }


        public async Task Add(Movie movie)
        {
              if (movie == null) throw new ArgumentNullException();
               await _context.AddAsync(movie);
                _context.SaveChanges();
        }






        public async Task Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

         public async Task<Movie?> Get(int? id)
        {
            return await _context.Movies.Include(m =>m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Movie>> ListAsync()
        {
            return await _context.Movies.ToListAsync();

        }

        public async Task Update()
        {
            await _context.SaveChangesAsync();
        }


    }
}
