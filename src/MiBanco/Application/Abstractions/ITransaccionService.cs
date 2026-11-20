using MiBanco.Application.Dtos;

namespace MiBanco.Application.Abstractions;

public interface ITransaccionService
{
    Task<IReadOnlyList<TransaccionDto>> GetAllAsync(CancellationToken ct = default);
    Task<TransaccionDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TransaccionDto> CreateAsync(TransaccionDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, TransaccionDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}