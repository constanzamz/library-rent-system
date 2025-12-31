using AutoMapper;
using Library.Dto.Request;
using Library.Dto.Response;
using Library.Entities.Models;

namespace Library.Services.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		
		CreateMap<OrderRequestDto, Order>()
			.ForMember(dest => dest.OrderBooks,
				opt => opt.MapFrom(src =>
					src.BookIds.Select(id => new OrderBook { BookId = id })));

		
		CreateMap<Order, OrderResponseDto>()
			.ForMember(dest => dest.Books,
				opt => opt.MapFrom(src =>
					src.OrderBooks.Select(ob => ob.Book)));
	}
}
