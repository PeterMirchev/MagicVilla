
using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Reservation;
using MagicVillaAPI.Models.Dto.User;
using MagicVillaAPI.Models.Dto.Villa;
using MagicVillaAPI.Models.Dto.Wallet;
using MagicVillaAPI.Utils;

namespace MagicVillaAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
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
            .ForMember(d => d.From, o => o.MapFrom(s => s.From.ToUtcDate()))
            .ForMember(d => d.To, o => o.MapFrom(s => s.To.ToUtcDate()));
        CreateMap<Reservation, ReservationResponse>();

        CreateMap<object, AuditRecord>();
    }
}