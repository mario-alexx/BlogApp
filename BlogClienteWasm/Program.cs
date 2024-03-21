using Blazored.LocalStorage;
using BlogClienteWasm;
using BlogClienteWasm.Servicios;
using BlogClienteWasm.Servicios.IServicio;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Agregar servicios
builder.Services.AddScoped<IPostServicio, PostServicio>();

// Usar Local Storage del navegador
builder.Services.AddBlazoredLocalStorage();
//Agregar para la autenticación y autorización
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s =>
    s.GetRequiredService<AuthStateProvider>()
);

await builder.Build().RunAsync();
