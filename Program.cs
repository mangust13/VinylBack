using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VinylBack.Context;
using VinylBack.Services;

namespace VinylBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Supabase PostgreSQL DB
            builder.Services.AddDbContext<VinylContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine);
            });

            // Services
            builder.Services.AddScoped<ISingerService, SingerService>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();
            builder.Services.AddScoped<ITrackService, TrackService>();
            builder.Services.AddScoped<IPurchasedTrackService, PurchasedTrackService>();
            builder.Services.AddScoped<ITrackInBasketService, TrackInBasketService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IAppUserService, AppUserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IPurchaseStatusService, PurchaseStatusService>();
            builder.Services.AddScoped<ILocationService, LocationService>();



            builder.Services.AddSingleton<IConfiguration>(provider => builder.Configuration);

            // Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VinylBack API", Version = "v1" });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
