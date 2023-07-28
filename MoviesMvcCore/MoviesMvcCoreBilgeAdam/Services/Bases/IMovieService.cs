using MoviesMvcCoreBilgeAdam.Models;

namespace MoviesMvcCoreBilgeAdam.Services.Bases
{
    public interface IMovieService
    {
        List<MovieModel> GetList();
        MovieModel GetDetails(int id);
        void Add(MovieModel model);
    }
}
