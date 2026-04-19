public interface IAuthService
{
    // Task<string?> LoginAsync(LoginDTO dto);
    // Task RegisterAsync(RegisterDTO dto);

     Task<string?> LoginAsync(LoginDTO dto);

     Task<string>  RegisterAsync(RegisterDTO dto);
}