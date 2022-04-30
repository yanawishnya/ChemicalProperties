using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ChemicalPropertiesApp.Models;

public class UserRepositorySqlServer : IUserRepository
{
    private readonly string _connectionString;

    public UserRepositorySqlServer(string conn)
    {
        _connectionString = conn;
    }

    public int DeleteUser(int id)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var sqlQuery = "DELETE FROM dbo.UsersInfo WHERE UserId=@id";
        return db.Execute(sqlQuery, new { id });
    }

    UsersInfo? IUserRepository.Get(int id)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return db.Query<UsersInfo>("SELECT * FROM dbo.UsersInfo WHERE UserId=@id", new { id }).FirstOrDefault();
    }
}