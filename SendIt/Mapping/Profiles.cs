using AutoMapper;
using SendIt.Dto;
using SendIt.Models;

namespace SendIt.Mapping
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<Book,BookDto>().ReverseMap();
            CreateMap<AddBookDto, Book>().ReverseMap();

        }
    }
}
