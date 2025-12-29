using Library.Entities.Models;

namespace Library.Repositories.Abstractions;

public interface IClientRepository : IRepositoryBase<Client>
{
	Task<Client?> GetByDniAsync(string dni);
}
