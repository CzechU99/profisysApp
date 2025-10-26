using AutoMapper;
using profisysApp.Entities;
using profisysApp.Models; 

namespace profisysApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentDto, Documents>();
            CreateMap<ItemDto, DocumentItems>(); 
        }
    }
}