using Microsoft.EntityFrameworkCore;
using BookAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;


namespace BookAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            string conStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BookContext>(options => options.UseSqlServer(conStr));
          


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // Add the origin of your React app
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
      
            var app = builder.Build();
            app.UseCors();
          //  app.UseDefaultServiceProvider(options => options.ChangeDefaultBrowser(BrowserNames.Chrome));

           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
      
            
           // app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.Use(async (context, next) =>
            {
                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Accept,Authorization");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,PATCH,OPTIONS,DELETE");
                    context.Response.StatusCode = 200;
                    return;
                }

                await next();
            });

           
            app.MapControllers();

            app.Run();
           
        }
    }
}