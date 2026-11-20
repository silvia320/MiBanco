using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MiBanco.Application.Abstractions;
using MiBanco.Domain.Common;

namespace MiBanco.Infrastructure.Persistence;

// LSP: cualquier Repository<T> puede sustituir a IRepository<T>.
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db;
    public Repository(AppDbContext db) => Db = db;

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await Db.Set<T>().AsNoTracking().OrderBy(x => x.Id).ToListAsync(ct);

    public async Task<IReadOnlyList<T>> GetAllWithAsync(string[] includes, CancellationToken ct = default)
    {
        IQueryable<T> query = Db.Set<T>().AsNoTracking();
        foreach (var include in includes) query = query.Include(include);
        return await query.OrderBy(x => x.Id).ToListAsync(ct);
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => Db.Set<T>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => Db.Set<T>().AnyAsync(predicate, ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await Db.Set<T>().AddAsync(entity, ct);
        await Db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        Db.Set<T>().Update(entity);
        await Db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        Db.Set<T>().Remove(entity);
        try
        {
            await Db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            throw new BusinessRuleException("No se puede eliminar: el registro tiene datos relacionados.");
        }
    }
}