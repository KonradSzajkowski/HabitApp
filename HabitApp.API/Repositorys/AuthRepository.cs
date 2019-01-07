using System.Threading.Tasks;
using HabitApp.API.Data;
using HabitApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace HabitApp.API.Repositorys
{
    public class AuthRepository : IAuthRepository
    {
        HabitAppDataContext context;
        IConfiguration configuration;
        public AuthRepository(HabitAppDataContext context,IConfiguration configuration){
            this.context=context;
            this.configuration=configuration;
        }

        public string GenerateToken(User user)
        {
            var claims =new []{
                new Claim("Id", user.Id.ToString())
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: creds
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> IsUserExist(string username)
        {
            var user=await context.User.FirstOrDefaultAsync(x => x.Username==username);
            if(user==null) return false;
            return true;
        }

        public async Task<User> Login(string username, string password)
        {
            var user=await context.User.FirstOrDefaultAsync(x => x.Username==username);

            if(user==null) return null;

            var hashCodeFromDB=user.PasswordHash;
            var passwordSaltFromDB=user.PasswordSalt;

            var hmac =new System.Security.Cryptography.HMACSHA512(passwordSaltFromDB);
            var hashCodeUsedToLogIn=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for(int i=0;i<hashCodeFromDB.Length;i++){
                if(hashCodeFromDB[i]!=hashCodeUsedToLogIn[i]) return null;
            }

            return user;

        }

        public async Task<User> Register(string username, string password,string email)
        {
            User user;
            using(var hmac =new System.Security.Cryptography.HMACSHA512()){ 
                    user=new User{
                    Username=username,
                    PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt=hmac.Key,
                    eMail=email
                    };
            }
            await context.User.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        
    }
}