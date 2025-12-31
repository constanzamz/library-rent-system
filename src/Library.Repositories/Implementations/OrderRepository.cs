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
				ob.Book.Status &&
				!ob.Order.IsReturned)
			.Select(ob => ob.Book)
			.Distinct()
			.ToListAsync();
	}

	


	public override async Task<Order> CreateAsync(Order entity)
	{
		var requestedBookIds = entity.OrderBooks.Select(x => x.BookId).Distinct().ToList();

		var books = await context.Books
			.Where(b => requestedBookIds.Contains(b.Id) && b.Status)
			.ToListAsync();

		if (books.Count != requestedBookIds.Count)
			throw new InvalidOperationException("Uno o más libros no existen o están eliminados.");

		if (books.Any(b => !b.IsAvailable))
			throw new InvalidOperationException("Uno o más libros ya están prestados.");

		foreach (var b in books)
			b.IsAvailable = false;

		context.Orders.Add(entity);
		await context.SaveChangesAsync();
		return entity;
	}

	public async Task ReturnAsync(int orderId)
	{
		var order = await context.Orders
			.Where(o => o.Id == orderId && o.Status)
			.Include(o => o.OrderBooks)
			.FirstOrDefaultAsync();

		if (order is null)
			throw new InvalidOperationException("Pedido no encontrado.");

		if (order.IsReturned)
			throw new InvalidOperationException("El pedido ya fue devuelto.");

		var bookIds = order.OrderBooks.Select(x => x.BookId).Distinct().ToList();

		var books = await context.Books
			.Where(b => bookIds.Contains(b.Id) && b.Status)
			.ToListAsync();

		foreach (var b in books)
			b.IsAvailable = true;

		order.IsReturned = true;

		await context.SaveChangesAsync();
	}

	public async Task<ICollection<Order>> GetAllWithBooksAsync()
	{
		return await context.Orders
			.Where(o => o.Status)
			.Include(o => o.OrderBooks)
				.ThenInclude(ob => ob.Book)
			.AsNoTracking()
			.ToListAsync();
	}

	

}
