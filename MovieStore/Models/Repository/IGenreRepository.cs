namespace MovieStore.Models.Repository
{
    public interface IGenreRepository
    {
        Genre Add(Genre movie);
        Genre Update(Genre movie);

        bool Delete(Genre movie);

       Task<List<Genre>> ListAsync();

        Genre Get(int id);

    }
}
