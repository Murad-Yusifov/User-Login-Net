using System.ComponentModel.DataAnnotations;
using RoleTypes.Enums;

public class RegisterDTO
{
    [Required]
    public required string Name { get; set; }

    [EmailAddress]
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }

    [Required]
    public required RoleType Role { get; set; } = RoleType.User;
}