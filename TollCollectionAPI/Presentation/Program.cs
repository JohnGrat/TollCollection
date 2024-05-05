using FastEndpoints;
using Presentation.Infrastructure;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints()
   .SwaggerDocument();
builder.Services
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();
