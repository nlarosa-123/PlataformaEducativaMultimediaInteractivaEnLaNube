using AutoMapper;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();

            CreateMap<CrearUsuarioDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
