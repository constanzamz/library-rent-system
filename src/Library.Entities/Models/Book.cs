using Library.Entities.Base;

namespace Library.Entities.Models;

public class Book : EntityBase
{
	public string Nombre { get; set; } = null!;
	public string Autor { get; set; } = null!;
	public string ISBN { get; set; } = null!;
	public bool IsAvailable { get; set; } = true;


}
