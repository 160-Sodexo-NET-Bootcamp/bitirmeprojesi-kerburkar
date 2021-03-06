using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Mapping.AutoMapper
{
    //entity'ler ve dto'lar arası map'leme için;
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<Colour, ColourDto>().ReverseMap();

            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<Offer, OfferListDto>().ReverseMap();

        }
    }
}
