using Library.Entities.Models;
using Library.Persistence.Context;
using Library.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<ICollection<Book>> GetBooksByClientDniAsync(string dni)
	{
		return await context.OrderBooks
			.Include(ob => ob.Book)
			.Include(ob => ob.Order)
				.ThenInclude(o => o.Client)
			.Where(ob =>
				ob.Order.Client.DNI == dni &&
				ob.Order.Status &&
				ob.Book.Status)
			.Select(ob => ob.Book)
			.Distinct()
			.ToListAsync();
	}
}
