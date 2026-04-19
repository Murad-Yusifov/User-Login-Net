// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("api/[controller]")]
// public class UserController : ControllerBase
// {
//     private readonly AppDbContext _context;

//     public UserController(AppDbContext context)
//     {
//         _context = context;
//     }

//     [HttpPost]
//     public IActionResult Create(User user)
//     {
//         if (!ModelState.IsValid)
//             return BadRequest(ModelState);

//         _context.User.Add(user);
//         _context.SaveChanges();

//         return Ok(user);
//     }

//     [HttpGet]
//     public IActionResult GetUsers(string? search)
//     {
//         var users = _context.User.AsQueryable();

//         if (!string.IsNullOrEmpty(search))
//         {
//             users = users.Where(u => u.Name.Contains(search));
//         }

//         return Ok(users.ToList());
//     }

//     [HttpPut("{id:int}")]
//     public IActionResult Change(int id, User user)
//     {
//         if(!ModelState.IsValid)
//         return BadRequest(ModelState);

//         var item = _context.User.Find(id);

//      if(item is null) return NotFound();

//      item.Name = user.Name;
//      item.Email = user.Email;
//      item.Password = user.Password;

//      _context.SaveChanges();

//      return Ok(item);

//     }




//     [HttpDelete("{id:int}")]
//     public IActionResult Delete(int id)
//     {

//         var user = _context.User.Find(id);

//         if(user is null) return NotFound();

//         _context.User.Remove(user);
//         _context.SaveChanges();

//         return NoContent();


//     }


//     [HttpPatch("{id:int}")]
//     public IActionResult ChangePass (int id, ChangePasswordDto dto)
//     {
//         var item =_context.User.Find(id);

//         if(item is null) return NotFound();

//         item.Password = dto.Password;
//         _context.SaveChanges();

//         return Ok("Password Succesfully changed");

//     }
// }


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IAuthService _authService;

    public UserController(IUserService service, IAuthService authService)
    {
        _service = service;
        _authService = authService;
    }

    [HttpGet]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> GetAll()
    {

    foreach (var c in User.Claims)
    {
        Console.WriteLine($"{c.Type} : {c.Value}");
    }
        var users = await _service.GetAll();
        return Ok(users);
    }


    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // [HttpPost]
    // [Authorize]
    // public async Task<IActionResult> CreateNewDataAsync(Experience experience)
    // {
    //     var experienced = _service.
    // }



    // [HttpPost]
    // public async Task<IActionResult> PostAsync(UserModelDTO dto)
    // {
    //     // var hashedPassword =_password.HashPassword(dto.Password);
    //     var user = new Users
    //     {
    //         Name=dto.Name,
    //         Email=dto.Email,
    //         Password=dto.Password,
    //     };


    //     var created = await _service.CreateAsync(user);
    //     return Ok(created);
    // }

    // [HttpPatch("{id:int}")]
    // public async Task<IActionResult> PatchAsync(int id, PasswordDTO dto)
    // {

    //     var result = await _service.PatchAsync(id, dto);

    //     if (result == null)
    //         return NotFound();

    //     return Ok(result);
    // }

    // [HttpDelete("{id:int}")]
    // public async Task<IActionResult> DeleteAsync(int id)
    // {
    //     var result = await _service.DeleteAsync(id);

    //     if (result == null)
    //         return NotFound();

    //     return NoContent();
    // }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDTO dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result is null)
            return Unauthorized("Invalid Email or Password");

        return Ok(new { token = result });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _authService.RegisterAsync(dto);
            return Ok(new { message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }

    }
}





