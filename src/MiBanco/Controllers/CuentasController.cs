using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Common;
using MiBanco.Extensions;

namespace MiBanco.Controllers;

public class CuentasController : Controller
{
    private readonly ICuentaService _service;
    private readonly IValidator<CuentaDto> _validator;
    private readonly IClienteService _svcCliente;
    private readonly ITipoCuentaService _svcTipoCuenta;

    public CuentasController(ICuentaService service, IValidator<CuentaDto> validator, IClienteService svcCliente, ITipoCuentaService svcTipoCuenta)
    {
        _service = service;
        _validator = validator;
        _svcCliente = svcCliente;
        _svcTipoCuenta = svcTipoCuenta;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _service.GetAllAsync(ct));

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        await LoadLookupsAsync(ct);
        return PartialView("_Modal", new CuentaDto { FechaApertura = DateOnly.FromDateTime(DateTime.Today) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CuentaDto dto, CancellationToken ct)
    {
        dto.Id = 0;
        (await _validator.ValidateAsync(dto, ct)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            await LoadLookupsAsync(ct);
            Response.StatusCode = 422;
            return PartialView("_Modal", dto);
        }

        await _service.CreateAsync(dto, ct);
        return Json(new { ok = true });
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        if (dto is null) return NotFound();
        await LoadLookupsAsync(ct);
        return PartialView("_Modal", dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CuentaDto dto, CancellationToken ct)
    {
        dto.Id = id;
        (await _validator.ValidateAsync(dto, ct)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            await LoadLookupsAsync(ct);
            Response.StatusCode = 422;
            return PartialView("_Modal", dto);
        }

        return await _service.UpdateAsync(id, dto, ct) ? Json(new { ok = true }) : NotFound();
    }

    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : PartialView("_DeleteModal", dto);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        try
        {
            await _service.DeleteAsync(id, ct);
            return Json(new { ok = true });
        }
        catch (BusinessRuleException ex)
        {
            return Json(new { ok = false, message = ex.Message });
        }
    }

    private async Task LoadLookupsAsync(CancellationToken ct)
    {
        ViewBag.ClienteItems = new SelectList(await _svcCliente.GetAllAsync(ct), "Id", "Texto");
        ViewBag.TipoCuentaItems = new SelectList(await _svcTipoCuenta.GetAllAsync(ct), "Id", "Texto");
    }
}