using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Villa;

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