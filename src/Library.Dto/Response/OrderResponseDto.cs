namespace Library.Dto.Response;

public record OrderResponseDto(
	int Id,
	DateTime FechaPedido,
	int ClientId
);
