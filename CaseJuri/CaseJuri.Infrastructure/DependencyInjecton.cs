using Amazon.DynamoDBv2;
using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;
using CaseJuri.Infrastructure.Dynamo;
using CaseJuri.Infrastructure.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CaseJuri.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        var useLocal = config.GetValue<bool>("Database:UseLocal");

        if (useLocal)
        {
            var mongoConn = config["Database:MongoConnectionString"];
            var mongoDb   = config["Database:MongoDatabaseName"];

            var mongoClient = new MongoClient(mongoConn);
            var database    = mongoClient.GetDatabase(mongoDb);

            BsonSerializer.RegisterSerializer(
                new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard)
            );

            if (!BsonClassMap.IsClassMapRegistered(typeof(ToDoTask)))
            {
                BsonClassMap.RegisterClassMap<ToDoTask>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.MapIdMember(c => c.Id);
                });
            }

            services.AddSingleton(database);
            services.AddScoped<IToDoTaskRepository, MongoToDoTaskRepository>();

            Console.WriteLine("Usando MongoDB local.");
        }
        else
        {
            var region = config["Database:DynamoRegion"];

            services.AddSingleton<IAmazonDynamoDB>(_ =>
                new AmazonDynamoDBClient(new AmazonDynamoDBConfig
                {
                    RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region)
                })
            );

            services.AddScoped<DynamoContext>();
            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
        }

        return services;
    }
}
