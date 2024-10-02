using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DesafioTecnicoAZGuidance;
using DesafioTecnicoAZGuidance.Services; // Adicione a referência ao namespace do serviço

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registre o HttpClient para ser utilizado pelos serviços
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5194/") });

// Registre o serviço PermissaoClienteService
builder.Services.AddScoped<PermissaoClienteService>();

await builder.Build().RunAsync();
