using Microsoft.EntityFrameworkCore;
using Npgsql;
using PMF.Database;
using PMF.Services;
using System.Reflection;
using System.Text.Json.Serialization;

var myLocalHostPolicy = "MyCORSPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myLocalHostPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader();
    });
});

builder.Services.AddScoped<CategoriesImporter>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase)
        );
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// DBContext registration
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(CreateConnectionString(builder.Configuration));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

}

app.UseAuthorization();

app.MapControllers();
app.UseCors(myLocalHostPolicy);
app.Run();

string CreateConnectionString(IConfiguration configuration)
{
    var username = Environment.GetEnvironmentVariable("DATABASE_USERNAME")?? "postgres";
    var pass = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "luka12345"; 
    var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "final";
    var host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
    var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";

    var connBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = int.Parse(port),
        Username = username,
        Database = databaseName,
        Password = pass,
        Pooling = true
    };

    return connBuilder.ConnectionString;
}




