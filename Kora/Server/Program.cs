using Kora_Transfert.Shared.Services;
using Kora.Server.Data;
using Kora.Server.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder.Services, builder.Configuration);

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
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAdministrateurService, AdministrateurService>();
        services.AddScoped<IAgenceService, AgenceService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ICompteService, CompteService>();
        services.AddScoped<IKiosqueService, KiosqueService>();
        services.AddScoped<IPaysService, PaysService>();
        services.AddScoped<IResponsableAgence, ResponsableAgenceService>();
        services.AddScoped<IVilleService, VilleService>();

        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddEndpointsApiExplorer();
        services.AddDbContext<KoraDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("KoraDbConnection")));

        // Ajouter AutoMapper
        services.AddAutoMapper(typeof(Program));

        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "Kora Api",
            });
        });
    }
}
