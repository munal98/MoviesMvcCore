using System.ComponentModel.DataAnnotations;

namespace MoviesMvcCoreBilgeAdam.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public short? ProductionYear { get; set; }

        public double? BoxOfficeReturn { get; set; }

        public List<MovieDirector> MovieDirectors { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
