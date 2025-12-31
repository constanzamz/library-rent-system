namespace Library.Dto.Response;

public record BookResponseDto(
	int Id,
	string Nombre,
	string Autor,
	string ISBN,
	bool IsAvailable
);
