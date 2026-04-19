public interface IUserService
{
     Task<List<Users>> GetAll();
     Task<Users?> GetById(int id);
     Task<Users?> GetByMailName(string email);
     
     Task<Users> CreateAsync(Users users);
     Task<Users?> UpdateAsync(int id, UserModelDTO user);
     Task<Users?> PatchAsync(int id, PasswordDTO password);

     Task<int?> DeleteAsync(int id);

    

}

