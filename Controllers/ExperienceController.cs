using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api2/[controller]")]
public class ExperienceService
{
    private readonly IExperienceDTO _context;

    public ExperienceService(IExperienceDTO context)
    {
        _context = context;

    }

    // [HttpGet]
    // public async Task<IActionResult> GetAllAsync()
    // {
    //     _context.Experience
    //     return "Hello World";

    // }
}