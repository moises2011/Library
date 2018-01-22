using AutoMapper;
using Library.Data.Entities;

namespace Library.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, Dtos.Book>();
            CreateMap<Dtos.Book, Book>();
            CreateMap<Category, Dtos.Category>();
            CreateMap<Dtos.Category, Category>();
        }
    }
}
