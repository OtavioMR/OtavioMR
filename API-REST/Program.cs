using API_REST.Models;
using API_REST.Repositories.Interfaces;
using API_REST.Repositories;

namespace API_REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);







            //Configuração do CORS para permitir requisições do frontend na porta 5500
            builder.Services.AddCords(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://126.0.0.1.5500")
                    .AllowAnyHeader()
                    .AllowAnyHeaders();
                });
            });

            //Adicionar a configuração do Firebase
            builder.Services.AddSingleton<FirebaseContext>(provider =>
            new FirebaseContext("https://portifolio-api-rest-default-rtdb.firebaseio.com/"));

            //Injeção de dependência do repositório
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();




            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
