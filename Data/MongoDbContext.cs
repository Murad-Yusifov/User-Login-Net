using MongoDB.Driver;
using TodoApi.BlogModel;

public class MongoDBContext
{
    private IMongoDatabase _database;

    public MongoDBContext (IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        _database = client.GetDatabase(config["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
}