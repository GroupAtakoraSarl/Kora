using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kora.Client;
using Kora.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Méthode ConfigureServices
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAdministrateurService, AdministrateurService>();
builder.Services.AddScoped<IAgenceService, AgenceService>();
builder.Services.AddScoped<ICompteService, CompteService>();
builder.Services.AddScoped<IKiosqueService, KiosqueService>();
builder.Services.AddScoped<IPaysService, PaysService>();
builder.Services.AddScoped<IResponsableAgence, ResponsableAgenceService>();
builder.Services.AddScoped<IVilleService, VilleService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.67:10000")});

await builder.Build().RunAsync();