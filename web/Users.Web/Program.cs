using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Refit;
using Users.Web.Infrastructure.Refit.Users;
using Users.Web.Manegement.Components;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRefitClient<IUsersApi>()
                .ConfigureHttpClient(configurator =>
                {
                    var baseAddress = configuration["WEB_API_BASE_ADRESS"]!;
                    configurator.BaseAddress = new Uri(baseAddress);
                });
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();