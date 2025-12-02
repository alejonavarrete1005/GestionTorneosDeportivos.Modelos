using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestionTorneos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GestionTorneosAPIContext>(options =>
                //options.UseSqlServer(builder.Configuration.GetConnectionString("GestionTorneosAPIContext.sqlserver") ?? throw new InvalidOperationException("Connection string 'GestionTorneosAPIContext' not found."))
                options.UseNpgsql(builder.Configuration.GetConnectionString("GestionTorneosAPIContext.postgres") ?? throw new InvalidOperationException("Connection string 'GestionTorneosAPIContext' not found."))
                //options.UseOracle(builder.Configuration.GetConnectionString("GestionTorneosAPIContext.oracle") ?? throw new InvalidOperationException("Connection string 'GestionTorneosAPIContext' not found."))
             //options.UseMySql(
             // builder.Configuration.GetConnectionString("GestionTorneosAPIContext.mariadb") ?? throw new InvalidOperationException("Connection string 'GestionTorneosAPIContext' not found."),
             //Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.7.2-MariaDB"))
             );

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configure JSON options
            builder.Services
                .AddControllers()
                .AddNewtonsoftJson(
                options =>
                options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
