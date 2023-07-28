using System.ComponentModel.DataAnnotations;

namespace MoviesMvcCoreBilgeAdam.Entities
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        public bool Retired { get; set; }

        public List<MovieDirector> MovieDirectors { get; set; }
    }
}
