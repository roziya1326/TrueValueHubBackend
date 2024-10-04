using AutoMapper;
using TrueValueHub.Models;

namespace TrueValueHub.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Part, Part>()
                .ForMember(dest => dest.InternalPartNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PartId, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore());
            CreateMap<Material, Material>()
                .ForMember(dest => dest.MaterialId, opt => opt.Ignore());


        }
    }
}
