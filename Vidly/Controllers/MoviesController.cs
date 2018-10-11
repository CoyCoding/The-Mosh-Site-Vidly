using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return Content("nothing");
            }

            var movie = GetMovies().SingleOrDefault(c => c.Id == id);
            if(movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie{Id = 1, Name = "Shrek"},
                new Movie{Id = 2, Name = "Shrek 2"}
            };
        }
    }
}