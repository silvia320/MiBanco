using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Services;

public class TipoCuentaService : ITipoCuentaService
{
    private readonly IRepository<TipoCuenta> _repo;

    public TipoCuentaService(IRepository<TipoCuenta> repo) => _repo = repo;

    public async Task<IReadOnlyList<TipoCuentaDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(Array.Empty<string>(), ct)).Select(ToDto).ToList();

    public async Task<TipoCuentaDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<TipoCuentaDto> CreateAsync(TipoCuentaDto dto, CancellationToken ct = default)
    {
        var entity = new TipoCuenta();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, TipoCuentaDto dto, CancellationToken ct = default)
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

    private static void Apply(TipoCuenta e, TipoCuentaDto d)
    {
        e.Nombre = d.Nombre.Trim();
        e.Descripcion = string.IsNullOrWhiteSpace(d.Descripcion) ? null : d.Descripcion.Trim();
        e.TasaInteres = d.TasaInteres;
        e.CostoMantenimiento = d.CostoMantenimiento;
    }

    private static TipoCuentaDto ToDto(TipoCuenta x) => new()
    {
        Id = x.Id,
        Nombre = x.Nombre,
        Descripcion = x.Descripcion,
        TasaInteres = x.TasaInteres,
        CostoMantenimiento = x.CostoMantenimiento,
    };
}