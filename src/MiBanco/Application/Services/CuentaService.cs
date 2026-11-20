using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Services;

public class CuentaService : ICuentaService
{
    private readonly IRepository<Cuenta> _repo;

    public CuentaService(IRepository<Cuenta> repo) => _repo = repo;

    public async Task<IReadOnlyList<CuentaDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(new[] { "Cliente", "TipoCuenta", "Transacciones" }, ct)).Select(ToDto).ToList();

    public async Task<CuentaDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<CuentaDto> CreateAsync(CuentaDto dto, CancellationToken ct = default)
    {
        var entity = new Cuenta();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, CuentaDto dto, CancellationToken ct = default)
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

    private static void Apply(Cuenta e, CuentaDto d)
    {
        e.NumeroCuenta = d.NumeroCuenta.Trim();
        e.FechaApertura = d.FechaApertura;
        e.ClienteId = d.ClienteId;
        e.TipoCuentaId = d.TipoCuentaId;
    }

    private static CuentaDto ToDto(Cuenta x) => new()
    {
        Id = x.Id,
        NumeroCuenta = x.NumeroCuenta,
        FechaApertura = x.FechaApertura,
        ClienteId = x.ClienteId,
        ClienteTexto = x.Cliente is null ? null : string.Join(" - ", x.Cliente.Cedula, x.Cliente.NombreCompleto),
        TipoCuentaId = x.TipoCuentaId,
        TipoCuentaTexto = x.TipoCuenta is null ? null : x.TipoCuenta.Nombre,
        Saldo = x.Saldo,
    };
}