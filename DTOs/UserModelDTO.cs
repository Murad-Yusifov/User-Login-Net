using System.ComponentModel.DataAnnotations;

public class UserModelDTO
{
    [Required]
    public required string Name {get; set;}
    [Required]
    [EmailAddress]
    public required string Email {get; set;}
[Required]
    public required string Password {get; set;}
}
