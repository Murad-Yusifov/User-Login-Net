using System.ComponentModel.DataAnnotations;

public class PasswordDTO
{
    [Required]
    public required string Password {get; set;} 
}