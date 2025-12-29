using Library.Entities.Base;

namespace Library.Repositories.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
	Task<ICollection<TEntity>> GetAsync();
	Task<TEntity?> GetByIdAsync(int id);
	Task<TEntity> CreateAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
	Task DeleteAsync(int id);
}
