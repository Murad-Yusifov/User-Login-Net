using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class AdminServise : IAdminService
{
    private readonly PasswordService _passwordService;
    private readonly AppDbContext _context;

    public AdminServise(PasswordService passwordService, AppDbContext context)
    {
        _passwordService = passwordService;
        _context = context;
    }

    public async Task<AdminDTO> CreateAsync(AdminDTO dto)
    {
        //  gets the data object 
        // returns the dta object as it was

        var admin = new AdminModel
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role,
            Position = dto.Position,
        };

         await _context.Admin.AddAsync(admin);
        await _context.SaveChangesAsync();

        return dto;



    }

    public async Task<AdminModel?> GetById(int id)
    {
        return await _context.Admin.FindAsync(id);
    }
    public async Task<List<AdminModel>> GetAll()
    {
      return await _context.Admin.ToListAsync();

       
    }

    public async  Task<AdminModel?> GetSingleUserAsnyc(int id)
    {
        return await GetById(id);
    }


}