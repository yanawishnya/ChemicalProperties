namespace ChemicalPropertiesApp.Models;

public interface IUserRepository
{
    UsersInfo? Get(int id);
    int DeleteUser(int id);
}