using Library.Entities.Models;

namespace Library.Repositories.Abstractions;

public interface IOrderRepository : IRepositoryBase<Order>
{
	Task<ICollection<Book>> GetBooksByClientDniAsync(string dni);
}
