using System;
using System.Collections.Generic;
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

        [HttpPost]
        public IHttpActionResult CreateMovieRental(MovieRentalDto movieRental)
        {
            var customer = _db.Customers.Single(c => c.Id == movieRental.CustomerId);

            var movies = _db.Movies.Where(m => movieRental.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
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
