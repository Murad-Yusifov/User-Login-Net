using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.BlogModel;

[ApiController]
[Route("api/[controller]")]
public class CommentController:ControllerBase
{
    private readonly ICommentService _commentServic;
    public CommentController(ICommentService commentService)
    {
        _commentServic =commentService;
    }

[HttpGet]
[Authorize]
public async Task<IActionResult>  GetAllAsync()
    {
        var comments = await _commentServic.GetAllDataAsync();

        return Ok(comments);
    }
    
}