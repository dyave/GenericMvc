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
            CreateMap<CustomerAddress, CustomerAddressMergedDto>()
                .ForMember(dest => dest.NameStyle, src => src.MapFrom(s => s.Customer.NameStyle))
                .ForMember(dest => dest.Title, src => src.MapFrom(s => s.Customer.Title))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(s => s.Customer.FirstName))
                .ForMember(dest => dest.MiddleName, src => src.MapFrom(s => s.Customer.MiddleName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(s => s.Customer.LastName))
                .ForMember(dest => dest.AddressLine1, src => src.MapFrom(s => s.Address.AddressLine1))
                .ForMember(dest => dest.AddressLine2, src => src.MapFrom(s => s.Address.AddressLine2))
                .ForMember(dest => dest.City, src => src.MapFrom(s => s.Address.City));
        }
    }
}
