using System.Threading.Tasks;
using HabitApp.API.Models;

namespace HabitApp.API.Repositorys
{
    public interface IAuthRepository
    {
        
         Task<User> Login(string username,string password);
         Task<User> Register(string username,string password,string email);
         
         string GenerateToken(User user);

         Task<bool> IsUserExist(string username);


    }
}