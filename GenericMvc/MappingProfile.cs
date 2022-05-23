using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericMvc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Product, Dtos.ProductDto>();
        }
    }
}
