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
                .ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductColor, act => act.MapFrom(src => src.Color))
                .ForMember(dest => dest.ProductStandardCost, act => act.MapFrom(src => src.StandardCost))
                .ForMember(dest => dest.ProductListPrice, act => act.MapFrom(src => src.ListPrice));

            CreateMap<Address, AddressDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerAddress, CustomerAddressDto>()
                .ForMember(dest => dest.AddressDto, act => act.MapFrom(src => src.Address))
                .ForMember(dest => dest.CustomerDto, act => act.MapFrom(src => src.Customer));

            CreateMap<CustomerAddress, CustomerAddressMergedDto>()
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Customer.CustomerId))
                .ForMember(dest => dest.NameStyle, act => act.MapFrom(src => src.Customer.NameStyle))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Customer.Title))
                .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.MiddleName, act => act.MapFrom(src => src.Customer.MiddleName))
                .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.AddressId, act => act.MapFrom(src => src.Address.AddressId))
                .ForMember(dest => dest.AddressLine1, act => act.MapFrom(src => src.Address.AddressLine1))
                .ForMember(dest => dest.AddressLine2, act => act.MapFrom(src => src.Address.AddressLine2))
                .ForMember(dest => dest.City, act => act.MapFrom(src => src.Address.City))
                .ReverseMap();

            CreateMap<Customer, CustomerComplexDto>()
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => new FullName {
                    FirstName = src.FirstName,
                    MiddleName = src.MiddleName,
                    LastName = src.LastName
                }))
                .ReverseMap()
                .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FullName.FirstName))
                .ForMember(dest => dest.MiddleName, act => act.MapFrom(src => src.FullName.MiddleName))
                .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.FullName.LastName))
                ;
        }
    }
}
