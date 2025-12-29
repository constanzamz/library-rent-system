namespace Library.Dto.Request;

public record ClientRequestDto(
	string Nombres,
	string Apellidos,
	string DNI,
	int Edad
);
