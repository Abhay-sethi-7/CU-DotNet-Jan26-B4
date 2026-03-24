using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartBank.Data;
using SmartBank.Repositories;
using SmartBank.Services;
using SmartBank.Services.Exceptions;

namespace SmartBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SmartBankContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SmartBankContext") ?? throw new InvalidOperationException("Connection string 'SmartBankContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionMiddleware>();

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
