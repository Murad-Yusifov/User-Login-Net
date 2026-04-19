// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly PasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public AuthService(IUserService userService, PasswordService passwordService, ITokenService tokenService)
    {
        _userService = userService;
        _passwordService = passwordService;
        _tokenService=tokenService;
    }


    public string GenerateToken(Users user)
    {
    //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345"));
    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //     var claims = new[]
    //     {
    //     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //     new Claim(ClaimTypes.Email, user.Email)
    // };

    //     var token = new JwtSecurityToken(
    //         claims: claims,
    //         expires: DateTime.UtcNow.AddHours(1),
    //         signingCredentials: creds
    //     );


    //     return new JwtSecurityTokenHandler().WriteToken(token);
    return _tokenService.GenerateToken(user);
    }

    public async Task<string> RegisterAsync(RegisterDTO dto)
    {
        var existing = await _userService.GetByMailName(dto.Email);

        if (existing != null)
            return "Email already exists";

        var user = new Users
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = _passwordService.HashPassword(dto.Password),
            Roles=dto.Role
        };

        await _userService.CreateAsync(user);

        // var allUsers = await _userService.GetAll();


        return $"Registered successfully";
    }

    // public async Task<string?> LoginAsync(LoginDTO dto)
    // {
    //     var user = await _userService.GetByMailName(dto.Email);

    //     if (user == null)
    //         return null;

    //     var isValid = _passwordService.VerifyPassword(dto.Password, user.Password);

    //     if (!isValid)
    //         return null;

    //     return GenerateToken(user);
    // }


    public async Task<string?> LoginAsync(LoginDTO dto)
{
    var user = await _userService.GetByMailName(dto.Email);

    if (user == null)
    {
        Console.WriteLine("USER NOT FOUND");
        return null;
    }

    Console.WriteLine("USER FOUND");

    // 👇 ADD DEBUG HERE
    Console.WriteLine("INPUT PASSWORD: " + dto.Password);
    Console.WriteLine("DB PASSWORD: " + user.Password);

    var isValid = _passwordService.VerifyPassword(dto.Password, user.Password);

    Console.WriteLine("PASSWORD VALID: " + isValid);

    if (!isValid)
        return null;

    return GenerateToken(user);
}

//     public async Task<string?> LoginAsync(LoginDTO dto)
// {
//     var user = await _userService.GetByMailName(dto.Email);

//     if (user == null)
//     {
//         Console.WriteLine("USER NOT FOUND");
//         return null;
//     }

//     Console.WriteLine("USER FOUND");

//     var isValid = _passwordService.VerifyPassword(dto.Password, user.Password);

//     Console.WriteLine($"PASSWORD VALID: {isValid}");

//     if (!isValid)
//         return null;

//     return GenerateToken(user);
// }



    // public async Task<string?> LoginAsync(LoginDTO dto)
    // {
    //     var user = await _userService.GetByMailName(dto.Email);

    //     if (user is null)
    //         return null;

    //     var isValid = _passwordService.VerifyPassword(dto.Password, user.Password);

    //     if (!isValid)
    //         return null;

    //     return "LOGIN_SUCCESS"; // later replace with JWT
    // }
}