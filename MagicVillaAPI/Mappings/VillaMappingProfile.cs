using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI.Mappings;

public class VillaMappingProfile : Profile
{
    public VillaMappingProfile()
    {
        CreateMap<VillaCreateRequest, Villa>();
        CreateMap<VillaUpdateRequest, Villa>();
        CreateMap<Villa, VillaResponse>();
    }
}