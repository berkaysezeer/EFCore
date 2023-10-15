using AutoMapper;
using EFCore.CodeFirst.DAL;
using EFCore.CodeFirst.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.Mappers
{
    //console uygulaması olduğu için bu konfigürasyonu yapmamız lazım
    public class ObjectMapper
    {
        //uygulama ayağa kalkınca çalışmayacak. çağrılınca yüklenecek
        //private olduğu için ObjectMapper üzerinden erişemeyiz
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        //ObjectMapper üzerinden erişebilmek için satırı ekliyoruz
        public static IMapper Mapper => lazy.Value;
    }

    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Product, ProductWithMapperDto>().ReverseMap();
        }
    }
}
