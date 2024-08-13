using FullLibrary.DTOs;
using FullLibrary.Models;

namespace FullLibrary.Mappings
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(source => source.Email))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.LastName))
                .DisableCtorValidation();

            CreateMap<UpdateProfileDto, User>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.LastName));

            CreateMap<AuthorDto, Author>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.LastName))
                .DisableCtorValidation();

            CreateMap<Author, AuthorDto>();

            CreateMap<AuthorUpdateDto, Author>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.LastName));
        }
    }
}
