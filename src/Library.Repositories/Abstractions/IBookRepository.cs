using Library.Entities.Models;

namespace Library.Repositories.Abstractions;

public interface IBookRepository : IRepositoryBase<Book>
{
	Task<ICollection<Book>> GetPaginatedAsync(int page, int pageSize);
}
