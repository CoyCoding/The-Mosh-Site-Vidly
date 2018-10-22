using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dto
{
    public class MovieRentalDto
    {
        public int CustomerID { get; set; }

        public List<int> MovieIds { get; set; }
    }
}