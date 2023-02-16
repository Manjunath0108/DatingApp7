using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
            opt => opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest =>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();

            CreateMap<MemberUpdateDto,AppUser>();

            CreateMap<RegisterDto,AppUser>();
            CreateMap<Message,MessageDto>()
                .ForMember(d=>d.SenderPhotoUrl,o=>o.MapFrom(s=>s.Sender.Photos.FirstOrDefault(x=>x.IsMain).Url))
                .ForMember(d=>d.RecipientPhotoUrl,o=>o.MapFrom(s=>s.Recipient.Photos.FirstOrDefault(x=>x.IsMain).Url));
        }
    }
}


/*
Map an "AppUser" object to a "MemberDto" object. The destination's "PhotoUrl" property is 
mapped from the source's main photo's "Url" property. The destination's "Age" property is mapped 
from the source's calculated age based on the "DateOfBirth" property.
Map a "Photo" object to a "PhotoDto" object.
Map a "MemberUpdateDto" object to an "AppUser" object.
*/