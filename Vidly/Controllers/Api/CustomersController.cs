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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _db;

        public CustomersController()
        {
            _db = new ApplicationDbContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_db.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        //Get /api/cutomers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //Post /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _db.Customers.Add(customer);
            _db.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //Put /api/customer/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);
            
            if(customerInDb == null)
            {
                NotFound();
            }

            Mapper.Map(customerDto, customerInDb);

            _db.SaveChanges();

            return Ok(customerDto);
        }

        //Delete /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customerInDb);
            _db.SaveChanges();

            var customerDto = Mapper.Map<Customer, CustomerDto>(customerInDb);

            return  Ok(customerDto);
        }
    }
}
