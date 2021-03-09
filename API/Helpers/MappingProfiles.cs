using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember( d => d.ProductBrand, o => o.MapFrom( s => s.ProductBrand.Name) )
                .ForMember( d => d.ProductType, o => o.MapFrom( s => s.ProductType.Name) )
                .ForMember( d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl))
                .ForMember( d => d.PictureUrl1, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl1))
                .ForMember( d => d.PictureUrl2, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl2))                
                .ForMember( d => d.PictureUrl3, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl3))                
                .ForMember( d => d.PictureUrl4, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl4))
                .ForMember( d => d.PictureUrl5, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl5))                
                .ForMember( d => d.PictureUrl6, o => o.MapFrom<ProductUrlResolver, string>(source => source.PictureUrl6));
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
           
    }
}  
