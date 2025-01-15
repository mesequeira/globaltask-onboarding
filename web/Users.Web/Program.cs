using FluentValidation;
using Fluxor;
using Microsoft.AspNetCore.Hosting;
using MudBlazor.Services;
using Refit;
using System.Reflection;
using Users.Web.Application.Users.Validators;
using Users.Web.Domain.Users.Services;
using Users.Web.Infrastructure.Refit.Users;
using Users.Web.Infrastructure.Users.Services;
using Users.Web.Manegement.Components;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

Assembly applicationAssembly = typeof(CreateUserModelValidator).Assembly;

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRefitClient<IRefitUsersApi>()
                .ConfigureHttpClient(configurator =>
                {
                    var baseAddress = configuration["WEB_API_BASE_ADRESS"]!;
                    configurator.BaseAddress = new Uri(baseAddress);
                })
                .ConfigurePrimaryHttpMessageHandler(sp =>
                {
                    return new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (
                            sender,
                            cert,
                            chain,
                            sslPolicyErrors
                        ) => true
                    };
                });

builder.Services.AddValidatorsFromAssembly(applicationAssembly);

builder.Services.AddMudServices();

builder.Services.AddFluxor(options => options.ScanAssemblies(applicationAssembly));

builder.Services.AddScoped<IUsersApi, UsersApi>();

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