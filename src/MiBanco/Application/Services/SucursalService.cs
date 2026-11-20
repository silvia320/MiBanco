using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Services;

public class SucursalService : ISucursalService
{
    private readonly IRepository<Sucursal> _repo;

    public SucursalService(IRepository<Sucursal> repo) => _repo = repo;

    public async Task<IReadOnlyList<SucursalDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(Array.Empty<string>(), ct)).Select(ToDto).ToList();

    public async Task<SucursalDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<SucursalDto> CreateAsync(SucursalDto dto, CancellationToken ct = default)
    {
        var entity = new Sucursal();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, SucursalDto dto, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        Apply(entity, dto);
        await _repo.UpdateAsync(entity, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        return true;
    }

    private static void Apply(Sucursal e, SucursalDto d)
    {
        e.Nombre = d.Nombre.Trim();
        e.Ciudad = d.Ciudad.Trim();
        e.Direccion = d.Direccion.Trim();
        e.Telefono = d.Telefono.Trim();
    }

    private static SucursalDto ToDto(Sucursal x) => new()
    {
        Id = x.Id,
        Nombre = x.Nombre,
        Ciudad = x.Ciudad,
        Direccion = x.Direccion,
        Telefono = x.Telefono,
    };
}