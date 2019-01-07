using System.Collections.Generic;
using System.Threading.Tasks;
using HabitApp.API.Models;

namespace HabitApp.API.Repositorys
{
    public interface IHabitRepository
    {
         Task<Habit> createHabit (int userId,string habitName);
         Task<List<string>> habitnames(int userId);
         Task<List<string>> habitData(int userId,string habitName);

         Task changePerformance(int userId,string habitName,string habitPerformance);
         Task<bool> isHabitExist(int userId,string habitName);
    }
}