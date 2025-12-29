using Library.Dto;
using Library.Dto.Response;
using Library.Dto.Request;

namespace Library.Services.Abstractions;

public interface IOrderService
{
	Task<BaseResponseGeneric<OrderResponseDto>> CreateAsync(OrderRequestDto dto);
	Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetBooksByClientDniAsync(string dni);
}
