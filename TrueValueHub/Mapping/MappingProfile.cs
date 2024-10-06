using AutoMapper;
using TrueValueHub.Dto;
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
            // Map ProjectDto to Project (including nested Parts)
            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.Parts, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())// Parts are mapped separately
                .ReverseMap();

            // Map PartDto to Part (and vice versa)
            CreateMap<PartDto, Part>()
                .ForMember(dest => dest.ChildParts, opt => opt.Ignore())
                .ReverseMap();

            // Map for nested ChildParts (handling recursive structure)
            CreateMap<Part, PartDto>()
                .ForMember(dest => dest.ChildParts, opt => opt.MapFrom(src => src.ChildParts))
                .ReverseMap();
           
              
        }

    
    }
}
