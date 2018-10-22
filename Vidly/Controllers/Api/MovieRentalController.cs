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
            var customer = _db.Customers.SingleOrDefault(c => c.Id == movieRental.CustomerId);

            if(customer == null)
            {
                return BadRequest("Invaild customer Id");
            }

            if(movieRental.MovieIds.Count == 0)
            {
                return BadRequest("No Movie Ids");
            }

            var movies = _db.Movies.Where(m => movieRental.MovieIds.Contains(m.Id)).ToList();

            if(movies.Count != movieRental.MovieIds.Count)
            {
                return BadRequest("One of the movie Ids was bad");
            }

            if(movies == null)

            foreach (var movie in movies)
            {
                    if(movie.QuantityAvailable == 0)
                    {
                        return BadRequest("Movie is not available");
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
