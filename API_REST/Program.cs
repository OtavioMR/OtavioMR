
using API_REST.Data;
using API_REST.Repositories;
using API_REST.Repositories.Interfaces;

namespace API_REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura��o do CORS para permitir requisi��es do frontend na porta 5500
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("allowFrontend", policy =>
                {
                    policy.WithOrigins("http://126.0.0.1:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            //Configura��o do Firebase
            builder.Services.AddSingleton<FirebaseContext>(provider =>
                new FirebaseContext("https://portifolio-api-rest-default-rtdb.firebaseio.com/"));



            // Inje��o de depend�ncia do reposit�rio
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
