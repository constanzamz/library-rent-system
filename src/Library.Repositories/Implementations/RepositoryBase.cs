using Library.Entities.Base;
using Library.Persistence.Context;
using Library.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
	where TEntity : EntityBase
{
	protected readonly ApplicationDbContext context;

	protected RepositoryBase(ApplicationDbContext context)
	{
		this.context = context;
	}

	public virtual async Task<ICollection<TEntity>> GetAsync()
	{
		return await context.Set<TEntity>()
			.Where(x => x.Status)
			.AsNoTracking()
			.ToListAsync();
	}

	public virtual async Task<TEntity?> GetByIdAsync(int id)
	{
		return await context.Set<TEntity>()
			.FirstOrDefaultAsync(x => x.Id == id && x.Status);
	}

	public virtual async Task<TEntity> CreateAsync(TEntity entity)
	{
		context.Set<TEntity>().Add(entity);
		await context.SaveChangesAsync();
		return entity;
	}

	public virtual async Task UpdateAsync(TEntity entity)
	{
		context.Set<TEntity>().Update(entity);
		await context.SaveChangesAsync();
	}

	public virtual async Task DeleteAsync(int id)
	{
		var entity = await context.Set<TEntity>()
			.FirstOrDefaultAsync(x => x.Id == id);

		if (entity is not null)
		{
			entity.Status = false;
			context.Set<TEntity>().Update(entity);
			await context.SaveChangesAsync();
		}
	}
}
