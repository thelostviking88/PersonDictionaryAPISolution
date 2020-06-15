using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Picture { get; set; }

        public virtual CityDto City { get; set; }
        public virtual ICollection<ConnectionDto> ConnecctedPersons { get; set; }
        public virtual ICollection<PhoneDto> Phone { get; set; }
    }
}