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
}
