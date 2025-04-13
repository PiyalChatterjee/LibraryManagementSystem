using AutoMapper;
using LMS.API.Models.Domain;
using LMS.API.Models.DTOs;
using LMS.API.Models.Enums;

namespace LMS.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role.ToString()))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status.ToString()))
                .ReverseMap()
                .ForMember(x => x.Role, opt => opt.MapFrom(x => Enum.Parse<UserRoles>(x.Role)))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => Enum.Parse<UserStatus>(x.Status)));
            CreateMap<AddUserRequestDTO, User>()
                .ForMember(x => x.Role, opt => opt.MapFrom(x => Enum.Parse<UserRoles>(x.Role)))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => Enum.Parse<UserStatus>(x.Status)))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x => DateTime.Now));

            CreateMap<UpdateUserRequestDTO, User>()
                .ForMember(x => x.Role, opt => opt.MapFrom(x => Enum.Parse<UserRoles>(x.Role)))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => Enum.Parse<UserStatus>(x.Status)));


            CreateMap<Member, MemberDTO>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status.ToString()))
                .ReverseMap()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => Enum.Parse<MemberStatus>(x.Status)));
            CreateMap<AddMemberRequestDTO, Member>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => Enum.Parse<MemberStatus>(x.Status)))
                .ReverseMap()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status.ToString()));
            CreateMap<UpdateMemberRequestDTO, Member>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => Enum.Parse<MemberStatus>(x.Status)))
                .ReverseMap()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status.ToString()));
        }
    }
}
