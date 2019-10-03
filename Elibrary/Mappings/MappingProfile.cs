using AutoMapper;
using Elibrary.Models;
public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<BooksDto, Books>();
    CreateMap<Books, BooksDto>();
  }
}