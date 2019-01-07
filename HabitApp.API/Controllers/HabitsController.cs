using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HabitApp.API.Repositorys;
using HabitApp.API.Dtos;
using System.Collections.Generic;

namespace HabitApp.API.Controllers
{

    
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]


    public class HabitsController:ControllerBase
    {
        IHabitRepository repository;
        public HabitsController(IHabitRepository repository)
        {
            this.repository=repository;
        }

        [HttpPost("createhabit")]
         public async Task<ActionResult> CreateHabit (HabitToCreate habit){
            var currentUser=HttpContext.User;
             if (currentUser.HasClaim(c => c.Type =="Id")){
                var userId=int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                if(await repository.isHabitExist(userId,habit.HabitName)) return BadRequest("this habit already exist");
                await repository.createHabit(userId,habit.HabitName);
                return StatusCode(201);
            }
            return BadRequest("error token definition");
       }

       [HttpGet("gethabitnames")]
       public async Task<ActionResult<IEnumerable<string>>> GetHabitNames(){
           var currentUser=HttpContext.User;
           if (currentUser.HasClaim(c => c.Type =="Id")){
               var userId=int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
               return Ok(await repository.habitnames(userId));
       }
        return BadRequest("error token definition");

    }
    [HttpPost("habitdata")]
    public async Task<ActionResult<IEnumerable<string>>> HabitData(HabitToGetHabitDataDto habit){
        var currentUser=HttpContext.User;
           if (currentUser.HasClaim(c => c.Type =="Id")){
               var userId=int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
               return Ok(await repository.habitData(userId,habit.HabitName));   
            }
        return BadRequest("error token definition");
    }

     [HttpPost("changeperformance")]
    public async Task<ActionResult> ChangePerformance(HabitToChangePerformanceDto habit){
         var currentUser=HttpContext.User;
           if (currentUser.HasClaim(c => c.Type =="Id")){
               var userId=int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
               await repository.changePerformance(userId,habit.HabitName,habit.Performance);
                return StatusCode(201);
             }
             return BadRequest("error token definition");
    }
}
}