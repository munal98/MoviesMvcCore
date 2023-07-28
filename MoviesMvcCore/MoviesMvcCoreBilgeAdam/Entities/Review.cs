using System.ComponentModel.DataAnnotations;

namespace MoviesMvcCoreBilgeAdam.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public byte Rating { get; set; }

        [StringLength(200)]
        public string Reviewer { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
