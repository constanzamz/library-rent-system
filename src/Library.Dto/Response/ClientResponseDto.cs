namespace Library.Dto.Response;

public record ClientResponseDto(
	int Id,
	string Nombres,
	string Apellidos,
	string DNI,
	int Edad
);
