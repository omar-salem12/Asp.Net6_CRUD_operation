namespace MovieStore.Models.Repository
{
    public interface IMovieRepository
    {
         
        Task  Add(Movie movie);
        Task Update();

        Task Delete(Movie movie);

        Task<List<Movie>> ListAsync();
       Task<Movie?> Get(int? id);

        
        
    }
}
