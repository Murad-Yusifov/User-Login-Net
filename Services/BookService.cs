public class BookService
{
    private readonly HttpClient _httpClient;
    private readonly string _key;

    public BookService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _key = config["GoogleBooks:ApiKey"]
        ?? throw new Exception("GoogleBooks API key is missing");
    }

    public async Task<string> SearchBook(string query)
    {
        var url = $"https://www.googleapis.com/books/v1/volumes?q={query}&key={_key}";
        return await _httpClient.GetStringAsync(url);

    }


}