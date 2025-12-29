using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Utils;

public static class QueryableExtensions
{
	public static async Task<List<T>> PaginateAsync<T>(
		this IQueryable<T> query,
		int page,
		int pageSize)
	{
		return await query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();
	}
}
