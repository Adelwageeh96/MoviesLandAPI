
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using Movies.Services.Common;
using Movies.Services.IServices;
using Movies.Services.Services;

namespace MoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbConnection");
            builder.Services.AddDbContext<ApplicationDbContext>
                (opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddHttpClient<ITmdbService, TmdbService>();
            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddAutoMapper(typeof(MovieProfile));

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.Urls.Add("http://0.0.0.0:5000");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
