using Library.Entities.Base;

namespace Library.Entities.Models;

public class Order : EntityBase
{
	public DateTime FechaPedido { get; set; } = DateTime.Now;

	public int ClientId { get; set; }
	public Client Client { get; set; } = null!;

	public ICollection<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
}