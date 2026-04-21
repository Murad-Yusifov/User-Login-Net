using TodoApi.BlogModel;

public interface ICommentService
{
    Task<List<Comment>> GetAllDataAsync();
}