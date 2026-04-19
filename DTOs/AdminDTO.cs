using System.ComponentModel.DataAnnotations;
using RoleTypes.Enums;

public class AdminDTO
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required PositionTypes Position { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(24)]
    public required string Password { get; set; }
    [Required]
    public required RoleType Role { get; set; } = RoleType.User;


}