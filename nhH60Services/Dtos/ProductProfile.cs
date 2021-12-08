using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using nhH60Services.Models;

namespace nhH60Services.Dtos {
    public class ProductProfile : Profile {
        public ProductProfile() {
            CreateMap<Product, ProductDTO>();
        }
    }
}
