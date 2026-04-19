
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminController:ControllerBase
{
    private readonly IAdminService _adminServise;
    // private readonly 
    public AdminController (IAdminService adminService)
    {
        _adminServise=adminService;
    }
    [HttpGet]
    public async Task<IActionResult> GetALl()
    {
       var admin = await _adminServise.GetAll();
        return Ok(admin);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(AdminDTO dto)
    {
        var data = await   _adminServise.CreateAsync(dto);
        if(data is null)
        return Unauthorized();

        return Ok(data);
    }
    [HttpGet("id")]
    public async Task<IActionResult?> GetSingleUser(int id)
    {
        var data = await _adminServise.GetSingleUserAsnyc(id);
        if(data is null)
        return NotFound();

        return Ok(new {Reult=data});

        
    }



}