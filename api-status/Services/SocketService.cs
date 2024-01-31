using MongoDB.Driver;

public class SocketService: ISocketService
{
    private readonly IMongoCollection<SocketConfig> _socketCollection;
    private readonly IMongoDatabase _database;

    public SocketService(MongoDbContext dbContext)
    {
        _database = dbContext.GetDatabase();
        _socketCollection = _database.GetCollection<SocketConfig>("SocketConfigs");
    }

    public Task<IList<ApiConfig>> GetApisBySocketIndex(int socketIndex)
    {
        throw new NotImplementedException();
    }

    public Task<SocketConfig> getBySocketIndex(int socketIndex)
    {
        throw new NotImplementedException();
    }
}
