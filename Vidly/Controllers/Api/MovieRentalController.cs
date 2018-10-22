using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dto;

namespace Vidly.Controllers.Api
{
    public class MovieRentalController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateMovieRental(MovieRentalDto movieRental)
        {
            throw new NotImplementedException();
        }
    }
}
