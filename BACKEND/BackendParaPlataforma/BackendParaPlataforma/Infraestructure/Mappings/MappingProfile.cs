using AutoMapper;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();

            CreateMap<CrearUsuarioCommand, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Emociones, EmocionesDto>();

            CreateMap<CrearEmocionCommand, Emociones>();

            CreateMap<DiarioEmocional, DiarioEmocionalDto>();

            CreateMap<CrearDiarioDto, DiarioEmocional>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Fecha, opt => opt.Ignore());
        }
    }
}