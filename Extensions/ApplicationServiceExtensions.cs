using Microsoft.EntityFrameworkCore;
using MyPortFolio.Data;
using MyPortFolio.Interfaces;
using MyPortFolio.Services;

namespace MyPortFolio.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
