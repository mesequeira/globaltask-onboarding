// using System.Reflection;

using System.Reflection;
using Users.Web.App;
using FluentValidation;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using MudBlazor.Services;
using Users.Web.Application.Users.Validations;
using Users.Web.Domain.DTO;
using Users.Web.Domain.Services;
using Users.Web.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);


Assembly applicationAssembly = typeof(UserDTOValidator).Assembly;

// Registrar Fluxor y escanear el ensamblado correcto donde están los reducers y effects
builder.Services.AddFluxor(options => options.ScanAssemblies(applicationAssembly).UseReduxDevTools());

// Registrar UserService (si aún no está registrado)
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
// builder.Services.AddFluxor(options =>
// {
//     options.ScanAssemblies(typeof(UserState).Assembly);
// });

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



builder.Services.AddMudServices();

// builder.Services.AddBlazoredFluentValidation();

builder.Services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();

builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();