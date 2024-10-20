using AutoMapper;
using SweetDictionary.Models.Entities;
using SweetDictionary.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetDictionary.Service.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreatePostRequestDto,Post>();
            CreateMap<Post, PostResponseDto>();
        }
    }
}
