using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Reservation;
using MagicVillaAPI.Models.Dto.User;
using MagicVillaAPI.Models.Dto.Villa;
using MagicVillaAPI.Models.Dto.Wallet;

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

        CreateMap<WalletCreateRequest, Wallet>();
        CreateMap<WalletUpdateRequest, Wallet>();
        CreateMap<Wallet, WalletResponse>();

        CreateMap<ReservationCreateRequest, Reservation>()
             .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.ToDateTime(TimeOnly.MinValue)))
             .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To.ToDateTime(TimeOnly.MinValue)));
        CreateMap<ReservationUpdateRequest, Reservation>();
        CreateMap<Reservation, ReservationResponse>();
    }
}