using RoleTypes.Enums;

public class AdminModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required PositionTypes Position { get; set; }
    
    public required string Password { get; set; }
    public required RoleType Role { get; set; } = RoleType.User;


}