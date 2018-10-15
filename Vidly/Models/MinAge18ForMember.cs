using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MinAge18ForMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == 1)
            {
                return ValidationResult.Success;
            }

            if(customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            return (customer.GetAge() >= 18) ? ValidationResult.Success : new ValidationResult($"You must be 18 for a {customer.MembershipTypeId} membership.");

        }
    }
}