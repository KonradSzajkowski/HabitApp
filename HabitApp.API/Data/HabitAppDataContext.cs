using System.Linq;
using HabitApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitApp.API.Data

{
    public class HabitAppDataContext: DbContext
    {
        public HabitAppDataContext(DbContextOptions<HabitAppDataContext> options)
            :base(options){}
        public DbSet<User> User {get; set;}
        public DbSet<Habit> Habit {get; set;}
    }
}