using PatrickJahr.Blazor.GrpcDevTools.WebApi.Models;
using PatrickJahr.Blazor.GrpcDevTools.WebApi.Services;
using PatrickJahr.Blazor.GrpcDevTools.WebApi.Utils;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;

var corsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("https://localhost:7014");
        });
});

builder.Services.AddDbContext<ConferencesDbContext>(
    options => options.UseInMemoryDatabase(databaseName: "ConfTool"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });
builder.Services.AddCodeFirstGrpcReflection();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<ConferencesDbContext>();
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicy);

app.UseRouting();
app.UseGrpcWeb();

app.UseAuthorization();

app.MapGrpcService<ConferencesService>().EnableGrpcWeb();
app.MapGrpcService<TimeService>().EnableGrpcWeb();
app.MapControllers();

app.Run();