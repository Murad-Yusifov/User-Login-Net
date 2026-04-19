using RoleTypes.Enums;

public class LoginDTO
{
    public required string Email {get; set;}
    public required string Password {get; set;}
    public required RoleType Role {get; set;} =RoleType.User;
}