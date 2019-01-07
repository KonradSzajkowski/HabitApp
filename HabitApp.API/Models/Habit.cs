using System;

namespace HabitApp.API.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string HabitName{ get; set; }

        public byte [] HabitPerformance {get; set;}
        public DateTime CreationDate {get; set;}
    }
}