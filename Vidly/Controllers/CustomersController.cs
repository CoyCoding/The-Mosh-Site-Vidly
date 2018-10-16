using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _db;

        public CustomersController()
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();   
        }

        public ActionResult New()
        {
            var membershipTypes = _db.MembershipTypes.ToList();

            var viewModel = new CustomerViewModel
            {
                Customer = new Customer { MembershipType = new MembershipType() },
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult CustomerForm()
        {
            var membershipTypes = _db.MembershipTypes.ToList();
            var customerViewModel = new CustomerViewModel
            {
                Customer = new Customer { MembershipType = new MembershipType() },
                MembershipTypes = membershipTypes
            };
            return View(customerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerViewModel = new CustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _db.MembershipTypes.ToList()
                };

                return View("CustomerForm", customerViewModel);
            }

            if(customer.Id == 0)
            {
                _db.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _db.Customers.Single(c => c.Id == customer.Id);

                // I would like to look into auto mapper
                // The reason for single sets is to stop injection.
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _db.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Index()
        {
            var customers = _db.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return Content("nothing");
            }

            var customer = _db.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _db.Customers.Include(c => c.MembershipType).SingleOrDefault( c => c.Id == id );
            
            if(customer == null)
            {
                return HttpNotFound();
            }

            var customerViewModel = new CustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _db.MembershipTypes.ToList()
            };

            return View("CustomerForm", customerViewModel);
        }
    }
}