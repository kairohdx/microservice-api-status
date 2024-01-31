using MongoDB.Driver;

public class ApiService : IApiService
{
    private readonly IMongoCollection<ApiConfig> _apiCollection; // Corrigido para ApiConfig

    public ApiService(MongoDbContext dbContext)
    {
        var database = dbContext.GetDatabase();
        _apiCollection = database.GetCollection<ApiConfig>("ApiConfigs");
    }
    
    public async Task<ApiConfig> GetBySocketIndex(int socketIndex)
    {
        var filter = Builders<ApiConfig>.Filter.Eq(x => x.SocketIndex, socketIndex);
        return await _apiCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IList<ApiConfig>> GetApisBySocketIndex(int socketIndex)
    {
        var filter = Builders<ApiConfig>.Filter.Eq(x => x.SocketIndex, socketIndex);
        return await _apiCollection.Find(filter).ToListAsync();
    }
}
