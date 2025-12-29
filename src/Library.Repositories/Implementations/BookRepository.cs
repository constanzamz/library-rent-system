using Library.Entities.Models;
using Library.Persistence.Context;
using Library.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Library.Repositories.Utils;

namespace Library.Repositories.Implementations;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
	public BookRepository(ApplicationDbContext context) : base(context)
	{
		
	}

	public async Task<ICollection<Book>> GetPaginatedAsync(int page, int pageSize)
	{
		return await context.Books
			.Where(b => b.Status)
			.AsNoTracking()
			.PaginateAsync(page, pageSize);
	}
}
