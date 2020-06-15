using System;
using System.Collections.Generic;

namespace API.Models
{
    public class PersonPostDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CityId { get; set; }
        public virtual ICollection<PhoneDto> Phone { get; set; }
    }
}