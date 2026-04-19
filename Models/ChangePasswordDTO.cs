using System.ComponentModel.DataAnnotations;

public class ChangePasswordDto
{
    [Required]
    [MinLength(6)]
    public required string Password {get; set;}
}