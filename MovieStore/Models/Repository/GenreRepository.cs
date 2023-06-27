using Microsoft.EntityFrameworkCore;
using MovieStore.Data;

namespace MovieStore.Models.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieDbContext _context;

        public GenreRepository(MovieDbContext context)
        {
            _context = context;
        }
        public Genre Add(Genre movie)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Genre movie)
        {
            throw new NotImplementedException();
        }

        public Genre Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Genre>> ListAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public Genre Update(Genre movie)
        {
            throw new NotImplementedException();
        }
    }
}
