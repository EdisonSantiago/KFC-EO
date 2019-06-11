using AutoMapper;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Api.Models.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            MapLocalToViewModel();
        }

        private void MapLocalToViewModel()
        {
            CreateMap<Local, LocalDto>();
            CreateMap<Estandar, EstandarDto>();
        }
    }
}