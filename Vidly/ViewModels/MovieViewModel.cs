using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Movie Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Quantity in Stock")]
        public int? QuantityInStock { get; set; }

        public MovieViewModel()
        {
            Id = 0;
        }

        public MovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            QuantityInStock = movie.QuantityInStock;
        }
    }
}