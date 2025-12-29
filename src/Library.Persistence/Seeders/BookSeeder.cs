using Library.Entities.Models;
using Library.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistence.Seeders;

public class BookSeeder
{
	private readonly IServiceProvider serviceProvider;

	public BookSeeder(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	public async Task SeedAsync()
	{
		using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

		// 📚 Libros a seedear (ISBN ya normalizado)
		var books = new List<Book>
		{
			new Book
			{
				Nombre = "Don Quijote de la mancha",
				Autor = "Miguel de Cervantes",
				ISBN = "9788437622149"
			},
			new Book
			{
				Nombre = "Cien años de soledad",
				Autor = "Gabriel García Márquez",
				ISBN = "9780307474728"
			},
			new Book
			{
				Nombre = "1984",
				Autor = "George Orwell",
				ISBN = "9780451524935"
			}
		};

		// ISBNs que quiero agregar
		var isbnToAdd = books.Select(b => b.ISBN).ToHashSet();

		// ISBNs que ya existen en la DB
		var existingIsbn = await context.Books
			.Where(b => isbnToAdd.Contains(b.ISBN))
			.Select(b => b.ISBN)
			.ToListAsync();

		// Filtrar libros que no existen
		var booksToAdd = books
			.Where(b => !existingIsbn.Contains(b.ISBN))
			.ToList();

		if (booksToAdd.Any())
		{
			await context.Books.AddRangeAsync(booksToAdd);
			await context.SaveChangesAsync();
		}
	}
}
