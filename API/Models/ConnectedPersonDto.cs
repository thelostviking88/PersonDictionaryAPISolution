using System;
using System.Collections.Generic;

namespace API.Models
{
    public class ConnectedPersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Picture { get; set; }

        public virtual CityDto City { get; set; }
        public virtual ICollection<PhoneDto> Phone { get; set; }
    }
}
