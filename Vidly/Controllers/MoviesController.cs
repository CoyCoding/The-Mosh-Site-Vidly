using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _db;

        public MoviesController()
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _db.Genres.ToList();

            var movieViewModel = new MovieViewModel
            {
                Genres = genres
            };

            return View("MovieForm", movieViewModel);
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View();
            }
            return View("PublicIndex");
        }

        public ActionResult Details(int? id)
        {
            try
            {
                var movieQuery = _db.Movies;

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
            var genres = _db.Genres.ToList();
            var movieViewModel = new MovieViewModel
            {
                Genres = genres
            };

            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieViewModel = new MovieViewModel(movie)
                {
                    Genres = _db.Genres.ToList()
                };
                return View("MovieForm", movieViewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.UtcNow;
                _db.Movies.Add(movie);
            }

            else
            {
                var MovieInDB = _db.Movies.Single(m => m.Id == movie.Id);

                MovieInDB.Id = movie.Id;
                MovieInDB.Name = movie.Name;
                MovieInDB.GenreId = movie.GenreId;
                MovieInDB.ReleaseDate = movie.ReleaseDate;
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _db.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new MovieViewModel(movie)
            {
                Genres = _db.Genres.ToList()
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