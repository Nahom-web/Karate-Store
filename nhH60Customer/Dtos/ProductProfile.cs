using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using nhH60Customer.Models;

namespace nhH60Customer.Dtos {
    public class ProductProfile : Profile {
        public ProductProfile() {
            CreateMap<Product, ProductDTO>();
        }
    }
}
