using AutoMapper;
using Library.Dto.Request;
using Library.Dto.Response;
using Library.Entities.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Services.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<OrderRequestDto, Order>()
			.ForMember(dest => dest.OrderBooks,
				opt => opt.MapFrom(src =>
					src.BookIds.Select(id => new OrderBook { BookId = id })));

		CreateMap<Order, OrderResponseDto>();
	}
}
