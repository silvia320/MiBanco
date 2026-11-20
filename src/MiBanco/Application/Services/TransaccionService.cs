using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Services;

public class TransaccionService : ITransaccionService
{
    private readonly IRepository<Transaccion> _repo;

    public TransaccionService(IRepository<Transaccion> repo) => _repo = repo;

    public async Task<IReadOnlyList<TransaccionDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(new[] { "Cuenta" }, ct)).Select(ToDto).ToList();

    public async Task<TransaccionDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<TransaccionDto> CreateAsync(TransaccionDto dto, CancellationToken ct = default)
    {
        var entity = new Transaccion();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, TransaccionDto dto, CancellationToken ct = default)
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

    private static void Apply(Transaccion e, TransaccionDto d)
    {
        e.CuentaId = d.CuentaId;
        e.Tipo = d.Tipo;
        e.Monto = d.Monto;
        e.Fecha = d.Fecha;
    }

    private static TransaccionDto ToDto(Transaccion x) => new()
    {
        Id = x.Id,
        CuentaId = x.CuentaId,
        CuentaTexto = x.Cuenta is null ? null : x.Cuenta.NumeroCuenta,
        Tipo = x.Tipo,
        Monto = x.Monto,
        Fecha = x.Fecha,
    };
}