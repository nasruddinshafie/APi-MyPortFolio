using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortFolio.Data;
using MyPortFolio.Entities;
using MyPortFolio.Interfaces;
using MyPortFolio.Models;
using System.Security.Cryptography;
using System.Text;

namespace MyPortFolio.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(RegisterModel registerModel) 
        {

            if ( await UserExist(registerModel.username)) return BadRequest("Useranme was taken");
           
            
            using var hmac = new HMACSHA512();


            var user = new AppUser
            {
                UserName = registerModel.username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerModel.password)),
                PasswordSalt = hmac.Key
            };

            _context.AppUsers.Add(user);
            _context.SaveChanges();

            return new UserModel
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),    
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(LoginModel loginModel)
        {
            var user = await _context.AppUsers.SingleOrDefaultAsync(x => x.UserName == loginModel.username);

            if (user == null) return Unauthorized();

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginModel.password));

            for(int i = 0; i< computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserModel
            {
                UserName = loginModel.username,
                Token = _tokenService.CreateToken(user)
            };

        }


        private async Task<bool> UserExist(string username)
        {
            return await _context.AppUsers.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
        }
       
    }
}
