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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _db.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //Get /api/cutomers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        //Post /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _db.Customers.Add(customer);
            _db.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }

        //Put /api/customer/1
        [HttpPut]
        public CustomerDto UpdateCustomer(int id, CustomerDto customerDto)
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

            Mapper.Map(customerDto, customerInDb);

            _db.SaveChanges();

            return customerDto;
        }

        //Delete /api/customers/1
        [HttpDelete]
        public CustomerDto DeleteCustomer(int id)
        {
            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _db.Customers.Remove(customerInDb);
            _db.SaveChanges();

            var customerDto = Mapper.Map<Customer, CustomerDto>(customerInDb);

            return customerDto;
        }
    }
}
