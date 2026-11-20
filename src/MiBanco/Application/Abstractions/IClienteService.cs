using MiBanco.Application.Dtos;

namespace MiBanco.Application.Abstractions;

public interface IClienteService
{
    Task<IReadOnlyList<ClienteDto>> GetAllAsync(CancellationToken ct = default);
    Task<ClienteDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ClienteDto> CreateAsync(ClienteDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, ClienteDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}