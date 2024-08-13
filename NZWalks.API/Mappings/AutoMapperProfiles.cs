using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SearchFilter, SearchFilterDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<AddCartItemRequestDto, CartItem>().ReverseMap();
            CreateMap<UpdateCartItemRequestDto, CartItem>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddUserRequestDto, User>().ReverseMap();
            CreateMap<UpdateUserRequestDto, User>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<AddOrderRequestDto, Order>().ReverseMap();
            CreateMap<UpdateOrderRequestDto, Order>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<AddOrderItemRequestDto, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderItemRequestDto, OrderItem>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AddProductRequestDto, Product>().ReverseMap();
            CreateMap<UpdateProductRequestDto, Product>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<AddProductCategoryRequestDto, ProductCategory>().ReverseMap();
            CreateMap<UpdateProductCategoryRequestDto, ProductCategory>().ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
