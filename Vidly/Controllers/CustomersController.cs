using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = GetCustomers();

            return View(customers);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return Content("nothing");
            }

            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{Id = 1, Name = "Joe Somebody"},
                new Customer{Id = 2, Name = "Jane Somebody"},
                new Customer{Id = 3, Name = "Albert Pipes"}
            };
        }
    }
}