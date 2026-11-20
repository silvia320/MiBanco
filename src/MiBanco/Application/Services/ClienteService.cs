using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IRepository<Cliente> _repo;

    public ClienteService(IRepository<Cliente> repo) => _repo = repo;

    public async Task<IReadOnlyList<ClienteDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(new[] { "Sucursal" }, ct)).Select(ToDto).ToList();

    public async Task<ClienteDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ClienteDto> CreateAsync(ClienteDto dto, CancellationToken ct = default)
    {
        var entity = new Cliente();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, ClienteDto dto, CancellationToken ct = default)
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

    private static void Apply(Cliente e, ClienteDto d)
    {
        e.Cedula = d.Cedula.Trim();
        e.NombreCompleto = d.NombreCompleto.Trim();
        e.Email = d.Email.Trim().ToLowerInvariant();
        e.SucursalId = d.SucursalId;
    }

    private static ClienteDto ToDto(Cliente x) => new()
    {
        Id = x.Id,
        Cedula = x.Cedula,
        NombreCompleto = x.NombreCompleto,
        Email = x.Email,
        SucursalId = x.SucursalId,
        SucursalTexto = x.Sucursal is null ? null : x.Sucursal.Nombre,
    };
}