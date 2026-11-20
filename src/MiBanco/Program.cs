using System.Globalization;
using FluentValidation;
using MiBanco.Application;
using MiBanco.Infrastructure;

// Carga .env (busca hacia arriba desde la carpeta actual). En Docker no existe y se usan las env del compose.
try { DotNetEnv.Env.NoClobber().TraversePath().Load(); } catch (FileNotFoundException) { }

// Mensajes por defecto de FluentValidation en español.
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    // La validacion la hace FluentValidation: evitamos el [Required] implicito de MVC para strings no nulos.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.ApplyMigrationsAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(e => e.Run(ctx => ctx.Response.WriteAsync("Error inesperado")));
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();