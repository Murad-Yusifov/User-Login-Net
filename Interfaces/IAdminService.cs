public interface IAdminService
{
    Task<AdminDTO> CreateAsync(AdminDTO dto);
    Task<List<AdminModel>> GetAll();
    Task<AdminModel?> GetSingleUserAsnyc(int id);
}