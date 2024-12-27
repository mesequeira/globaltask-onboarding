// using MediatR;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using System.Reflection;
//
// namespace MiProyecto
// {
//     public class Startup
//     {
//         public void ConfigureServices(IServiceCollection services)
//         {
//             // Agregar MediatR y escanear el ensamblado actual para los handlers
//             services.AddMediatR(Assembly.GetExecutingAssembly());
//
//             // Otras configuraciones de servicios, como controladores, etc.
//             services.AddControllers();
//         }
//
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }
//             else
//             {
//                 app.UseExceptionHandler("/Home/Error");
//                 app.UseHsts();
//             }
//
//             app.UseHttpsRedirection();
//             app.UseStaticFiles();
//
//             app.UseRouting();
//
//             app.UseAuthorization();
//
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers(); // Mapea los controladores
//             });
//         }
//     }
// }