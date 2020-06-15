using API.Models;
using AutoMapper;
using Data.Models;

namespace API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Person, PersonDto>().ForMember(dest =>
            dest.ConnecctedPersons,
            opt => opt.MapFrom(src => src.PersonConnectionPerson));
            CreateMap<City, CityDto>();
            CreateMap<Phone, PhoneDto>();
            CreateMap<PersonConnection, ConnectionDto>();
            CreateMap<Person, ConnectedPersonDto>();

            CreateMap<PersonPutDto, Person>();
            CreateMap<PersonPostDto, Person>();
            CreateMap<CityDto, City>();
            CreateMap<PhoneDto, Phone>();
        }
    }
}
