namespace Library.Dto.Response;

public class OrderResponseDto
{
	public int Id { get; set; }
	public DateTime FechaPedido { get; set; }
	public int ClientId { get; set; }
	public bool IsReturned { get; set; }
	public ICollection<BookResponseDto> Books { get; set; } = new List<BookResponseDto>();
}
