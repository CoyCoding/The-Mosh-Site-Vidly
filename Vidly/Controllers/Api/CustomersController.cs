using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _db;

        public CustomersController()
        {
            _db = new ApplicationDbContext();
        }

        //GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _db.Customers.ToList();
        }

        //Get /api/cutomers/1
        public Customer GetCustomer(int id)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        //Post /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _db.Customers.Add(customer);
            _db.SaveChanges();

            return customer;
        }

        //Put /api/customer/1
        [HttpPut]
        public Customer UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);
            
            if(customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            _db.SaveChanges();

            return customer;
        }

        //Delete /api/customers/1
        [HttpDelete]
        public Customer DeleteCustomer(int id)
        {
            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _db.Customers.Remove(customerInDb);
            _db.SaveChanges();

            return customerInDb;
        }
    }
}
