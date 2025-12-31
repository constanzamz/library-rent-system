using Library.Entities.Models;
using Library.Persistence.Context;
using Library.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class ClientRepository : RepositoryBase<Client>, IClientRepository
{
	public ClientRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<Client?> GetByDniAsync(string dni)
	{
		return await context.Clients
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.DNI == dni && c.Status);
	}

	public async Task<ICollection<Client>> SearchByNameAsync(string term, int take = 20)
	{
		term = term?.Trim() ?? string.Empty;

		var query = context.Clients
			.AsNoTracking()
			.Where(c => c.Status);

		if (!string.IsNullOrWhiteSpace(term))
		{
			query = query.Where(c =>
				EF.Functions.Like(c.Nombres, $"%{term}%") ||
				EF.Functions.Like(c.Apellidos, $"%{term}%"));
		}

		return await query
			.OrderBy(c => c.Apellidos)
			.ThenBy(c => c.Nombres)
			.Take(take)
			.ToListAsync();
	}

}
