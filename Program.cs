using Catalog.Repositories;
using Catalog.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Catalog;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            return new MongoClient(settings.ConnectionString);
        });

        builder.Services.AddSingleton<IItemsRepository, MongoDBItemsRepository>(); // Dependency injection yaptýk

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