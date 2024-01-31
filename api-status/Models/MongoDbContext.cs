using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDBConnection");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("DataBase");
    }

    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}
