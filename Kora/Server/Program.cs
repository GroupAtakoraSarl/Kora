using System.Text.Json.Serialization;
using Kora.Server.Data;
using Kora.Server.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //CreateHostBuilder(args).Build().Run();
        
        builder.Services.AddDbContext<KoraDbContext>(opt=>
            opt.UseSqlite(builder.Configuration.GetConnectionString("KoraDbConnection")));

        builder.Services.AddControllers();
        ConfigureServices(builder.Services);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "Kora Api",
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.Run();
    }

    // Separate method to configure services
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAdministrateurService, AdministrateurService>();
        services.AddScoped<IAgenceService, AgenceService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ICompteService, CompteService>();
        services.AddScoped<IKiosqueService, KiosqueService>();
        services.AddScoped<IResponsableAgence, ResponsableAgenceService>();
        services.AddScoped<IPaysService, PaysService>();
        services.AddScoped<IVilleService, VilleService>();
        services.AddScoped<ITransactionService, TransactionService>();
    
        services.AddAutoMapper(typeof(Program));
        
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddEndpointsApiExplorer();
        
    }
}
