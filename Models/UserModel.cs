// public class User
// {
//     public int Id { get; set; }
//     public string Name { get; set; } = "";
//     public string Email { get; set; } = "";
//     public DateTime CreatedAt { get; set; } =DateTime.UtcNow;
// }

// public class User
// {
//     public int Id { get; set; }

//     [Required]
//     public string Name { get; set; }

//     [Required]
//     [EmailAddress]
//     public string Email { get; set; }

//     [MinLength(6)]
//     public string Password { get; set; }
// }

using System.ComponentModel.DataAnnotations;
using RoleTypes.Enums;

public class Users
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
    [Required]
    public required RoleType Roles { get; set; }
}