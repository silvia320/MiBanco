using MiBanco.Application.Dtos;

namespace MiBanco.Application.Abstractions;

public interface ICuentaService
{
    Task<IReadOnlyList<CuentaDto>> GetAllAsync(CancellationToken ct = default);
    Task<CuentaDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<CuentaDto> CreateAsync(CuentaDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, CuentaDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}