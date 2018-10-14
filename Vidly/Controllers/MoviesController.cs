using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include( m => m.Genre ).ToList();

            return View(movies);
        }

        public ActionResult Details(int? id)
        {
            try
            {
                var movieQuery = _context.Movies;

                id = ValidateIntId(id, movieQuery);

                var movie = movieQuery.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

                return View(movie);
            }
            catch (NullReferenceException)
            {
                return HttpNotFound();
            }
        }

        public ActionResult MovieForm()
        {
            var genres = _context.Genres.ToList();
            var movieViewModel = new MovieViewModel
            {
                Genres = genres
            };

            return View(movieViewModel);
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {


            return View();
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", movieViewModel);
        }

        #region Handling of bad Ids
        //
        // This region changes the page to the nearest value if no such id exists
        // This section is no longer needed but left because this isn't production code
        //
        private int ValidateIntId(int? id, IQueryable<Movie> queryable)
        {
            id = id.GetValueOrDefault();
            var movie = queryable.ToList().LastOrDefault();

            if (movie != null)
            {
                if (id >= movie.Id)
                {
                    return FindLastIndex(id, queryable);
                }
                else
                {
                    return FindNextIndex(id, queryable);
                }
            }
            else
            {
                throw new NullReferenceException("Movie Query found no movies");
            }
        }
        
        private int FindNextIndex(int? id, IQueryable<Movie> queryable)
        {
            if(queryable.SingleOrDefault(q=> q.Id == id) != null)
            {
                return (int)id;
            }
            else
            {
                return FindNextIndex(id + 1, queryable);
            } 
        }

        private int FindLastIndex(int? id, IQueryable<Movie> queryable)
        {
            if (queryable.SingleOrDefault(q => q.Id == id) != null)
            {
                return (int)id;
            }
            else
            {
                return FindLastIndex(id - 1, queryable);
            }
        }
        #endregion
    }
}