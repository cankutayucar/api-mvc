using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Dtos;
using Architecture.Core.Entities;
using AutoMapper;

namespace Architecture.Service.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
