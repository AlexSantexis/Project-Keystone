using AutoMapper;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Api.Models.DTOs.AddressDTos;
using Project_Keystone.Api.Models.DTOs.BasketDTOs;
using Project_Keystone.Api.Models.DTOs.OrderDTOs;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Project_Keystone.Api.Models.DTOs.UserDTOs;
using Project_Keystone.Api.Models.DTOs.Wishlist;
using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            //User Mappers
            CreateMap<User,UserLoginDTO>().ReverseMap();
            CreateMap<User, UserDetailedDTO>().ReverseMap();
            CreateMap<UserRegisterDTO,User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();


            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<UserUpdatePasswordDTO, User>().ReverseMap();

            // Product Mappers

            CreateMap<ProductDTO, Product>().ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.ProductGenres.Select(pg => pg.Genre)))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.ImageUrl));



            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.ProductCategories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new ProductCategory { CategoryId = id })))
                .ForMember(dest => dest.ProductGenres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new ProductGenre { GenreId = id })))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImgUrl));



            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.ProductCategories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new ProductCategory { CategoryId = id })))
                .ForMember(dest => dest.ProductGenres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new ProductGenre { GenreId = id })));
                

            // Category Mappers
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // Genre Mappers
            CreateMap<Genre, GenreDTO>().ReverseMap();

            //Wishlist Mappers
            CreateMap<Wishlist,WishlistDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.WishListItems));

            CreateMap<WishlistDTO, Wishlist>()
                .ForMember(dest => dest.WishListItems, opt => opt.MapFrom(src => src.Items));
            
            CreateMap<WishlistItem, WishlistItemDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt));
            
            //Address Mapper
            CreateMap<AddAddressDTO, Address>().ReverseMap();
            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<Address, AddressDetailedDTO>().ReverseMap();

            //Basket Mapper
            CreateMap<Basket, BasketDTO>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.BasketItems));
            CreateMap<BasketItem, BasketItemDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Product.ImageUrl));
            CreateMap<AddToBasketDTO, BasketItem>();


            //Order Mapper
            CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<CreateOrderDTO, Order>();

        }

    }
}
