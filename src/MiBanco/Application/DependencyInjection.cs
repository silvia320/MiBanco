using FluentValidation;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Services;

namespace MiBanco.Application;

public static class DependencyInjection
{
     public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registra automaticamente todos los AbstractValidator<T> del proyecto.
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<ISucursalService, SucursalService>();
        services.AddScoped<ITipoCuentaService, TipoCuentaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<ICuentaService, CuentaService>();
        services.AddScoped<ITransaccionService, TransaccionService>();
        return services;
    }
}