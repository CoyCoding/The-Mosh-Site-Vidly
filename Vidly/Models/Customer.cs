using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
        
        [Display(Name = "Date of Birth")]
        [MinAge18ForMember]
        public DateTime? Birthdate { get; set; }

        public int? GetAge() {
            int? age = null;
            if (Birthdate != null)
            {
                age = DateTime.Now.Year - Birthdate.Value.Year;
                if (age == 18)
                {
                    if (DateTime.Now.Month < Birthdate.Value.Month || (DateTime.Now.Month == Birthdate.Value.Month && DateTime.Now.Day < Birthdate.Value.Day))
                    {
                        age -= 1;
                    }
                }
            }
            return age;
        }
    }
}