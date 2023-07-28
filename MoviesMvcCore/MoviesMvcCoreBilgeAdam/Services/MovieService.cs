using Microsoft.EntityFrameworkCore;
using MoviesMvcCoreBilgeAdam.Entities;
using MoviesMvcCoreBilgeAdam.Models;
using MoviesMvcCoreBilgeAdam.Services.Bases;
using System.Globalization;

namespace MoviesMvcCoreBilgeAdam.Services
{
    public class MovieService : IMovieService
    {
        //MovieContext _db = new MovieContext(); // objelerin new'lenerek kullanılması yerine ASP.NET Core'da Constructor Injection (Dependency Injection) kullanılır.
        private readonly DbContext _db;

        public MovieService(DbContext db)
        {
            _db = db;
        }

        public List<MovieModel> GetList()
        {
            try
            {
                return _db.Set<Movie>().OrderBy(m => m.Name).Select(m => new MovieModel()
                {
                    Id = m.Id,
                    BoxOfficeReturn = m.BoxOfficeReturn,
                    Name = m.Name,
                    ProductionYear = m.ProductionYear,

                    //BoxOfficeReturnModel = m.BoxOfficeReturn.HasValue ? m.BoxOfficeReturn.Value.ToString("C2", new CultureInfo("tr-TR")) : "0"
                    //BoxOfficeReturnModel = m.BoxOfficeReturn.HasValue ? m.BoxOfficeReturn.Value.ToString("C2", new CultureInfo("en-US")) : "0"
                    BoxOfficeReturnModel = (m.BoxOfficeReturn ?? 0).ToString("C2", new CultureInfo("en-US"))
                }).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public MovieModel GetDetails(int id)
        {
            try
            {
                MovieModel model = null;

                // Movie <-> MovieDirectors <-> Director

                // Many to many ilişkilerde (A entity, AB entities, B entity) A entity'si çekilirken
                // ilişkili AB entity'leri Include(a => a.AB) ile, B entity'si ise ThenInclude(ab => ab.B)
                // ile çekilir.
                // One to many ilişkilerde (A entity, B entities) A entity'si çekilirken
                // ilişkili B entity'leri Include(a => a.B) ile çekilir.
                // One to one ilişkilerde (A entity, B entity) A entity'si çekilirken
                // ilişkili B entity'si Include(a => a.B) ile çekilir.
                Movie entity = _db.Set<Movie>().Include(m => m.Reviews).Include(m => m.MovieDirectors)
                    .ThenInclude(md => md.Director).SingleOrDefault(m => m.Id == id);

                if (entity != null)
                {
                    model = new MovieModel()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        BoxOfficeReturn = entity.BoxOfficeReturn,
                        BoxOfficeReturnModel = (entity.BoxOfficeReturn ?? 0).ToString("C2", new CultureInfo("en-US")),
                        ProductionYear = entity.ProductionYear,
                        ReviewCountModel = entity.Reviews.Count,
                        ReviewAverageModel = entity.Reviews.Average(r => r.Rating).ToString("N1", new CultureInfo("en-US")),

                        // 2. yöntem:
                        DirectorsModel = string.Join("<br />", entity.MovieDirectors.Select(md => md.Director.Name + " " + md.Director.Surname))
                    };

                    // 1. yöntem:
                    //model.DirectorsModel = "";
                    //if (entity.MovieDirectors != null && entity.MovieDirectors.Count > 0)
                    //{
                    //    foreach (MovieDirector movieDirector in entity.MovieDirectors)
                    //    {
                    //        model.DirectorsModel += movieDirector.Director.Name + " " + movieDirector.Director.Surname + "<br />";
                    //    }

                    //    //model.DirectorsModel = model.DirectorsModel.Substring(0, model.DirectorsModel.Length - 6);
                    //    //model.DirectorsModel = model.DirectorsModel.TrimEnd(new char[] { '<', 'b', 'r', ' ', '/', '>' });
                    //    //model.DirectorsModel = model.DirectorsModel.TrimEnd('<', 'b', 'r', ' ', '/', '>');
                    //    model.DirectorsModel = model.DirectorsModel.TrimEnd("<br />".ToCharArray());
                    //}
                }
                return model;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Add(MovieModel model)
        {
            try
            {
                Movie entity = new Movie()
                {
                    Name = model.Name.Trim(),
                    ProductionYear = model.ProductionYear,

                    //BoxOfficeReturn = string.IsNullOrWhiteSpace(model.BoxOfficeReturnModel) ? null:
                    //Convert.ToDouble(model.BoxOfficeReturnModel, new CultureInfo("en-US"))
                    BoxOfficeReturn = string.IsNullOrWhiteSpace(model.BoxOfficeReturnModel) ? null :
                    Convert.ToDouble(model.BoxOfficeReturnModel.Replace(",", "."), 
                    CultureInfo.InvariantCulture)
                };
                _db.Set<Movie>().Add(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
