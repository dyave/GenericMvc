using AutoMapper;
using GenericMvc.Dtos;
using GenericMvc.Models;
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
            CreateMap<Product, ProductDto>();

            //ProductId and ProductNumber can be omitted:
            CreateMap<Product, ProductDiffDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(s => s.Name))
                .ForMember(dest => dest.ProductColor, src => src.MapFrom(s => s.Color))
                .ForMember(dest => dest.ProductStandardCost, src => src.MapFrom(s => s.StandardCost))
                .ForMember(dest => dest.ProductListPrice, src => src.MapFrom(s => s.ListPrice));

            CreateMap<Address, AddressDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerAddress, CustomerAddressDto>()
                .ForMember(dest => dest.AddressDto, src => src.MapFrom(s => s.Address))
                .ForMember(dest => dest.CustomerDto, src => src.MapFrom(s => s.Customer));
        }
    }
}
