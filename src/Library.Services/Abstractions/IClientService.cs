using Library.Dto;
using Library.Dto.Request;
using Library.Dto.Response;

namespace Library.Services.Abstractions;

public interface IClientService
{
	Task<BaseResponseGeneric<ICollection<ClientResponseDto>>> GetAsync();
	Task<BaseResponseGeneric<ClientResponseDto>> GetByIdAsync(int id);
	Task<BaseResponseGeneric<ClientResponseDto>> CreateAsync(ClientRequestDto dto);
	Task<BaseResponse> UpdateAsync(int id, ClientRequestDto dto);
	Task<BaseResponse> DeleteAsync(int id);
	Task<BaseResponseGeneric<ICollection<ClientResponseDto>>> SearchAsync(string term, int take = 20);

}
