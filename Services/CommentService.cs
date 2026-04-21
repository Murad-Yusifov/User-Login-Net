using Microsoft.EntityFrameworkCore;
using TodoApi.BlogModel;

public class CommentService: ICommentService

{
    private readonly AppDbContext   _context;

    public CommentService (AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllDataAsync()
    {
       return  await  _context.Comments.ToListAsync();
    }
}