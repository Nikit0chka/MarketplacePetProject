using Entities.DataBase;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using Repositories.RepositoryInterfaces;
using Services;
using Services.ServiceInterfaces;

namespace Authentication.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<RefreshTokenContext>(options => options.UseSqlServer("Server=DESKTOP-L1TCS06;Database=RefreshTokenDataBase;Integrated Security=True;Encrypt=False"));

        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();

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