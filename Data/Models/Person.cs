using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Person
    {
        public Person()
        {
            PersonConnectionConnectedPerson = new HashSet<PersonConnection>();
            PersonConnectionPerson = new HashSet<PersonConnection>();
            Phone = new HashSet<Phone>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CityId { get; set; }
        public string Picture { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<PersonConnection> PersonConnectionConnectedPerson { get; set; }
        public virtual ICollection<PersonConnection> PersonConnectionPerson { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }
}
