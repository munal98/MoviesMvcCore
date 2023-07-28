using Microsoft.AspNetCore.Mvc;
using MoviesMvcCoreBilgeAdam.Models;
using MoviesMvcCoreBilgeAdam.Services.Bases;
using System.Globalization;

namespace MoviesMvcCoreBilgeAdam.Controllers
{
    // Ekstra kaynaklar:
    // https://httpstatuses.com/
    // https://automapper.org/
    // https://autofac.org/
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index() // ~/Movies/Index
        {
            List<MovieModel> model = _movieService.GetList();
            return View(model);
        }

        public IActionResult Details(int? id) // ~/Movies/Details/1
        {
            //if (!id.HasValue)
            if (id == null)
            {
                //return new BadRequestResult();
                //return BadRequest();
                return BadRequest("id is required!"); // 400
            }
            MovieModel model = _movieService.GetDetails(id.Value);
            if (model == null)
            {
                //return NotFound();
                return NotFound("Movie not found!"); // 404
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() // ~/Movies/Create
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Name, short? ProductionYear, string BoxOfficeReturnModel)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new ContentResult()
                {
                    Content = "<label style=\"color: red;\">" +
                    "Movie name is required!</label><br />" +
                    "<a href=\"/Movies/Create\">Create Movie</a>", ContentType = "text/html"
                };
            double boxOfficeReturn;
            if (!string.IsNullOrWhiteSpace(BoxOfficeReturnModel) && !double.TryParse(BoxOfficeReturnModel.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out boxOfficeReturn))
                return Content("<label style=\"color: red;\">" +
                    "Box office return must be numeric!</label><br />" +
                    "<a href=\"/Movies/Create\">Create Movie</a>", "text/html");

            MovieModel model = new MovieModel()
            {
                Name = Name,
                ProductionYear = ProductionYear,
                BoxOfficeReturnModel = BoxOfficeReturnModel
            };
            _movieService.Add(model);
            //return RedirectToAction("Index", "Movies");
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id) // ~Movies/Edit/1
        {
            if (id == null)
            {
                return View("MyError", "Id is required!");
            }
            MovieModel model = _movieService.GetDetails(id.Value);
            if (model == null)
                return View("MyError", "Movie not found!"); 
            return View(model);
        }
        
        #region Bir aksiyondan dönülebilecek IActionResult'ı implemente eden tipler:
        /*
         IActionResult
        ActionReuslt
        ViewResult(View) -- ContentResult  (Content))- RedirectResults - HttpResults -JsonResults(Json()) - FileContentResult - EmptyResult
        */
        public ContentResult  GetHtmlContent()
        {
            return Content("<b><i>Content result.</i></b>", "text/html");
        }

        public string GetString()
        {
            return "String.";
        }

        public EmptyResult GetEmpty()
        {
            return new EmptyResult();
        }

        public JsonResult GetMoviesJsonContent() //Movies/GetMoviesJsonContent
        {
            List<MovieModel> movies = _movieService.GetList();
            return Json(movies);
        }

        public ContentResult GetMoviesXmlContent() //~/Movies/GetMoviesXmlContent
        {
            List<MovieModel> movies = _movieService.GetList();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            xml += "<MovieModels";
            foreach (MovieModel movie in movies)
            {
                xml += "<MovieModel>";
                xml += "<Id>" + movie.Id + "</Id>";
                xml += "<Name>" + movie.Name + "</Name>";
                xml += "<ProductionYear>" + movie.ProductionYear + "</ProductionYear>";
                xml += "<BoxOfficeReturn>" + movie.BoxOfficeReturn + "</BoxOfficeReturn>";
                xml += "<BoxOfficeReturnModel>" + movie.BoxOfficeReturnModel + "</BoxOfficeReturnModel>";
                xml += "<DirectorsModel>" + movie.DirectorsModel + "</DirectorsModel>";
                xml += "<ReviewCountModel>" + movie.ReviewCountModel + "</ReviewCountModel>";
                xml += "<ReviewAverageModel>" + movie.ReviewAverageModel + "</ReviewAverageModel>";
                xml += "</MovieModel>";

            }
            xml += "</MovieModels>";
            return Content(xml, "application/xml");
        }
        #endregion
    }
}
