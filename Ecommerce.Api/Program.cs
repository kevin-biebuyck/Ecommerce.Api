using Ecommerce.Api.Controllers;
using Ecommerce.Api.Data;

namespace Ecommerce.Api
{
    public class Program
    {
        private const string CorsPolicy = "CorsPolicy";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ECommerceContext>();
            builder.Services.AddCors(c => c.AddPolicy(CorsPolicy, policyBuilder =>
            {
                policyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            }));

            var app = builder.Build();
            app.UseCors(CorsPolicy);
            app.UseSwagger();
            app.UseSwaggerUI();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            };

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapProductEndpoints();

            app.Run();
        }
    }
}