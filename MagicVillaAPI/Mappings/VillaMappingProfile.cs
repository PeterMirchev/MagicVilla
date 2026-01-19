using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.User;
using MagicVillaAPI.Models.Dto.Villa;

namespace MagicVillaAPI.Mappings;

public class VillaMappingProfile : Profile
{
    public VillaMappingProfile()
    {
        CreateMap<VillaCreateRequest, Villa>();
        CreateMap<VillaUpdateRequest, Villa>();
        CreateMap<Villa, VillaResponse>();

        CreateMap<UserCreateRequest, User>();
        CreateMap<UserUpdateRequest, User>();
        CreateMap<User, UserResponse>();
    }
}