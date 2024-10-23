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

            // Configuração do CORS para permitir requisições do frontend na porta 5500
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("allowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500") // Altere se necessário
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            // Configuração do Firebase
            builder.Services.AddSingleton<FirebaseContext>(provider =>
                new FirebaseContext("https://portifolio-api-rest-default-rtdb.firebaseio.com/"));

            // Injeção de dependência do repositório
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();

            // Adiciona os serviços ao contêiner
            builder.Services.AddControllers();
            // Configuração do Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configuração do pipeline de requisições HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("allowFrontend"); // Habilita a política de CORS aqui
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
