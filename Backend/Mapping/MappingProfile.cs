using AutoMapper;
using DTOs_AutoMapper.DTOs.UserDTO;
using DTOs_AutoMapper.DTOs.ProductDTO;
using DTOs_AutoMapper.Models;
using DTOs_AutoMapper.DTOs.CartDTO;

namespace DTOs_AutoMapper.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User,Userread>();
            CreateMap<Usercreate,User>();
            CreateMap<Userupdate,User>();

            CreateMap<Product,Productread>();
            CreateMap<Productcreate,Product>();
            CreateMap<Productupdate,Product>();

            CreateMap<Cart, Cartread>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.products.ProductName))
                .ForMember(dest => dest.ProductImg, opt => opt.MapFrom(src => src.products.ProductImg))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.products.price))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.users.UserName));
            CreateMap<Cartcreate, Cart>();
            CreateMap<Cartupdate, Cart>();

        }
    }
}
