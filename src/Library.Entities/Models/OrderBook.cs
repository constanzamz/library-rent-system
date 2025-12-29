namespace Library.Entities.Models;

public class OrderBook
{
	public int OrderId { get; set; }
	public Order Order { get; set; } = null!;

	public int BookId { get; set; }
	public Book Book { get; set; } = null!;
}
