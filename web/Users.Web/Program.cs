using Users.Web.App;
using Blazored.FluentValidation;
using FluentValidation;
using MudBlazor.Services;
using Users.Web.Application.Users.Commands.Validations;
using Users.Web.Domain.DTO;
using Users.Web.Domain.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

// builder.Services.AddBlazoredFluentValidation();

builder.Services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();


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