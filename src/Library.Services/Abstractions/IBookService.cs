using Library.Dto;
using Library.Dto.Request;
using Library.Dto.Response;

namespace Library.Services.Abstractions;

public interface IBookService
{
	Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync();
	Task<BaseResponseGeneric<BookResponseDto>> GetByIdAsync(int id);
	Task<BaseResponseGeneric<BookResponseDto>> CreateAsync(BookRequestDto dto);
	Task<BaseResponse> UpdateAsync(int id, BookRequestDto dto);
	Task<BaseResponse> DeleteAsync(int id);
	Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetPaginatedAsync(int page, int pageSize);

}

