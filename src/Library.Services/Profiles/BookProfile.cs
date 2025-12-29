using AutoMapper;
using Library.Dto.Request;
using Library.Dto.Response;
using Library.Entities.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Services.Profiles;

public class BookProfile : Profile
{
	public BookProfile()
	{
		CreateMap<BookRequestDto, Book>();
		CreateMap<Book, BookResponseDto>();
	}
}
