using MiBanco.Application.Dtos;

namespace MiBanco.Application.Abstractions;

public interface ITipoCuentaService
{
    Task<IReadOnlyList<TipoCuentaDto>> GetAllAsync(CancellationToken ct = default);
    Task<TipoCuentaDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TipoCuentaDto> CreateAsync(TipoCuentaDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, TipoCuentaDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}