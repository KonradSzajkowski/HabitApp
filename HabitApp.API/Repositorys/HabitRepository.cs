
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.API.Data;
using HabitApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitApp.API.Repositorys
{
    public class HabitRepository : IHabitRepository
    {
        
    HabitAppDataContext context;

    public HabitRepository(HabitAppDataContext context)
    {
        this.context=context;
    }


        public async Task<Habit> createHabit(int userId, string habitName)
        {
            var performance=new byte [2];
            performance[0]=0;
            var curentDate= DateTime.Now;

            var habit=new Habit{
                UserId=userId,
                HabitName=habitName,
                HabitPerformance=performance,
                CreationDate=curentDate
            };

            await context.Habit.AddAsync(habit);
            await context.SaveChangesAsync();

            return habit;
        }

        public async Task<List<string>> habitData(int userId, string habitName)
        {

            var habit =await context.Habit.FirstOrDefaultAsync(x => x.UserId==userId && x.HabitName==habitName);
            List<string> list=new List<string>();
            list.Add(habit.CreationDate.ToString());
            list.Add(System.Text.Encoding.Default.GetString(habit.HabitPerformance));
            return list;
        }

        public async Task<List<string>> habitnames(int userId)
        {
            var result= await (from h in context.Habit
                        where h.UserId == userId
                        select h.HabitName).ToListAsync();
            return result;
        }

        public async Task<bool> isHabitExist(int userId, string habitName)
        {
            var habit =await context.Habit.FirstOrDefaultAsync(x => x.UserId==userId && x.HabitName==habitName);
            if(habit==null) return false;
            return true;
        }

         public async Task changePerformance(int userId,string habitName,string habitPerformance){
             var habit =await context.Habit.FirstOrDefaultAsync(x => x.UserId==userId && x.HabitName==habitName);
             habit.HabitPerformance=
                System.Text.Encoding.Default.GetBytes(habitPerformance);
                await context.SaveChangesAsync();
         }
    }
}