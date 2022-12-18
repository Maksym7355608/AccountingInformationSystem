using AccountingInformationSystem.Administration.DataModels;

namespace AccountingInformationSystem.Administration.Interfaces
{
    public interface IAuthService
    {
        Task AddNewUserAsync(UserDataModel user);
        Task<string> LogInAsync(string username, string password);
        Task UpdateUserInfoAsync(UserDataModel user);
        Task DeleteUserAsync(long id);
    }
}
