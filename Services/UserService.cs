
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{

    private readonly PasswordService _passwordService;
    private readonly AppDbContext _context;

    public UserService(AppDbContext context, PasswordService passwordService)
    {
        _passwordService = passwordService;
        _context = context;
    }



    private async Task<Users?> FindUserAsync(int id)
    {

        return await _context.Users.FindAsync(id);
    }

    public async Task<List<Users>> GetAll()
    {
        return await _context.Users.ToListAsync();

    }

    public async Task<Users?> GetById(int id)
    {
        // return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        return await FindUserAsync(id);



    }

    public async Task<Users?> GetByMailName(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<Users> CreateAsync(Users users)
    {

        // var hashedPassword = _passwordService.HashPassword(users.Password);

        var user = new Users
        {
            Name = users.Name,
            Email = users.Email,
            Password = users.Password,
            Roles=users.Roles
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<Users?> UpdateAsync(int id, UserModelDTO inputUser)
    {

        // var item = await _context.User.FindAsync(id);
        var item = await FindUserAsync(id);

        if (item is null)
            return null;

        item.Name = inputUser.Name;
        item.Email = inputUser.Email;
        item.Password = inputUser.Password;

        await _context.SaveChangesAsync();

        return item;

    }

    public async Task<int?> DeleteAsync(int id)
    {
        //    var item = await _context.User.FindAsync(id);
        var item = await FindUserAsync(id);


        if (item is null)
        {
            return null;
        }

        _context.Users.Remove(item);

        await _context.SaveChangesAsync();

        return item.Id;
    }

    public async Task<Users?> PatchAsync(int id, PasswordDTO user)
    {
        // var item = await _context.User.FindAsync(id);
        var item = await FindUserAsync(id);

        if (item is null)
            return null;

        item.Password = _passwordService.HashPassword(user.Password);

        await _context.SaveChangesAsync();

        return item;

    }

    public async Task<Experience> CreateNewExperienceAsync(Experience experience)
    {
        _context.Experience.Add(experience);
        await _context.SaveChangesAsync();

        return experience;
    }
}
