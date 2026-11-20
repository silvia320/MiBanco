using MiBanco.Application.Dtos;

namespace MiBanco.Application.Abstractions;

public interface ISucursalService
{
    Task<IReadOnlyList<SucursalDto>> GetAllAsync(CancellationToken ct = default);
    Task<SucursalDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<SucursalDto> CreateAsync(SucursalDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, SucursalDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}