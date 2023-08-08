using EmployeesAPI2.Application.Filters;
using EmployeesAPI2.Application.Mappers;
using EmployeesAPI2.Application.Mappers.interfaces;
using EmployeesAPI2.Application.Services;
using EmployeesAPI2.Application.Services.Interfaces;
using EmployeesAPI2.Application.Settings;
using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using EmployeesAPI2.Infrastructure.Repository;
using MongoDB.Driver;

namespace EmployeesAPI2
{
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

            // Configuramos la politica de CORS
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // * Configura la inyección de dependencias para MediatR
            builder.Services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly));

            // * Obtenemos las configuraciones de mongo desde el appsettings al modelo mongoSettings
            MongoSettings mongoSettings = new();
            builder.Configuration.GetSection(mongoSettings.SectionName)
                .Bind(mongoSettings);

            // * Crear el cliente de mongo y obtner la base de datos que vamos a usar
            MongoClient mongoClient = new(mongoSettings.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(mongoSettings.Database);

            // * Configuramos en el inyector de dependencias la conexión a la base de datos de mongo
            builder.Services.AddSingleton(service =>
                database.GetCollection<Employee>(mongoSettings.Collections.Employees));
            builder.Services.AddSingleton(service =>
                database.GetCollection<User>(mongoSettings.Collections.Users));

            // * Configuramos la inyeccion de los repositorios
            builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IEmployeeMappers, EmployeeMappers>();
            builder.Services.AddSingleton<IAuthentificationService, AuthentificationService>();
            builder.Services.AddScoped<AuthorizationFilter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Usamos la politica que configuramos previamente
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}