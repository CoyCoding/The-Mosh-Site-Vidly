using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MovieRentalController : ApiController
    {
        private ApplicationDbContext _db;

        public MovieRentalController()
        {
            _db = new ApplicationDbContext();
        }

        public IHttpActionResult GetMovieRentals()
        {
            var movieRentals = _db.RentalInfos.Include(ri => ri.Movie).Include(ri => ri.Customer).ToList();

            return Ok(movieRentals);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovieRental(MovieRentalDto movieRental)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == movieRental.CustomerId);

            if (customer == null)
            {
                return BadRequest("Invalid CustomerId");
            }

            if (movieRental.MovieIds.Count == 0)
            {
                return BadRequest("No movies selected");
            }

            var movies = _db.Movies.Where(m => movieRental.MovieIds.Contains(m.Id)).ToList();

            if(movieRental.MovieIds.Count != movies.Count)
            {
                BadRequest("One or more invalid MovieIds");
            }

            foreach (var movie in movies)
            {
                if (movie.QuantityAvailable == 0)
                {
                    BadRequest("Movie is out of stock");
                }

                --movie.QuantityAvailable;

                var movieToRent = new RentalInfo
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _db.RentalInfos.Add(movieToRent);
            }

            _db.SaveChanges();
            return Ok();
        }
    }
}
