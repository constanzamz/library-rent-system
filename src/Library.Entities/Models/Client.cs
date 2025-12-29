using Library.Entities.Base;

namespace Library.Entities.Models;

public class Client : EntityBase
{
	public string Nombres { get; set; } = null!;
	public string Apellidos { get; set; } = null!;
	public string DNI { get; set; } = null!;
	public int Edad { get; set; }
}