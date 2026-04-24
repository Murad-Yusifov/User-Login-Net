
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

public class UserService : IUserService
{

    private readonly PasswordService _passwordService;
    private readonly IMongoCollection<Users> _context;

    public UserService(IMongoDatabase database, PasswordService passwordService)
    {
        _passwordService = passwordService;
        _context = database.GetCollection<Users>("Users");
    }



    private async Task<Users?> FindUserAsync(int id)
    {

        return await _context.Find(u=>u.Id==id).FirstOrDefaultAsync();
    }

    public async Task<List<Users>> GetAll()
    {
        return await _context.Find(_=>true).ToListAsync();

    }

    public async Task<Users?> GetById(int id)
    {
        // return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        return await FindUserAsync(id);



    }

    public async Task<Users?> GetByMailName(string email)
    {
        return await _context.Find(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
    }

    public async Task<Users> CreateAsync(Users users)
    {

        // var hashedPassword = _passwordService.HashPassword(users.Password);

        // var user = new Users
        // {
        //     Name = users.Name,
        //     Email = users.Email,
        //     Password = users.Password,
        //     Roles=users.Roles
        // };
        await _context.InsertOneAsync(users);

        return users;
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

        await _context.DeleteOneAsync(u=>u.Id==id);

        return item.Id;
    }

    public async Task<Users?> PatchAsync(int id, PasswordDTO user)
    {
        // var item = await _context.User.FindAsync(id);
        var item = await FindUserAsync(id);

        if (item is null)
            return null;

        item.Password = _passwordService.HashPassword(user.Password);

        return item;

    }

    public async Task<Experience> CreateNewExperienceAsync(Experience experience)
    {
        // _context.Experience.Add(experience);
       

        return experience;
    }
}
