using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dto;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _db;

        public MoviesController()
        {
            _db = new ApplicationDbContext();
        }

        //GET api/movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_db.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        //GET Api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //Post api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            movieDto.DateAdded = DateTime.Now;
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _db.Movies.Add(movie);
            _db.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        //Put api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movie);

            _db.SaveChanges();

            return Ok(movieDto);
        }

        //DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _db.Movies.SingleOrDefault(m => m.Id == id);

            if(movieInDb == null)
            {
                return NotFound();
            }

            _db.Movies.Remove(movieInDb);
            _db.SaveChanges();

            var movieDto = Mapper.Map<Movie, MovieDto>(movieInDb);
            return Ok(movieDto);
        }
    }
}
