using alahaly_momken.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using YourNamespace;

namespace alahaly_momken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files")),
                RequestPath = "/Upload/Files"
            });
            app.UseHttpsRedirection();

            app.UseStaticFiles(); // Add this line to serve static files

            app.UseRouting();

            // Enable CORS
            app.UseCors("AllowOrigin");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
