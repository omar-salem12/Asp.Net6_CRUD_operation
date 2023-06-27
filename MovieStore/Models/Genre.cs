using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Models
{
    public class Genre
    {


        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public List<Movie> Movies { get; set; }

    }
}
