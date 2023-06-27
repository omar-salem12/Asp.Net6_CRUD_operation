using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required,MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }

        [Required,StringLength(255)]
        public string? Storeline { get; set; }

        [Required]
        public byte[]? Poster { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }




    }
}
