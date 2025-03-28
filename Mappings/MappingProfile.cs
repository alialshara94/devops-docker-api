using AutoMapper;
using PersonApi.DTOs;
using PersonApi.Models;

namespace PersonApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from Person to PersonDto
            CreateMap<Person, PersonDto>();
            
            // Map from CreatePersonDto to Person
            CreateMap<CreatePersonDto, Person>();
            
            // Map from UpdatePersonDto to Person
            CreateMap<UpdatePersonDto, Person>();
        }
    }
}