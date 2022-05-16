using System.Security.Cryptography;
using System.Text;
using api.Context;
using api.Controllers.BaseController;
using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ApplicationContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(ApplicationContext context, ITokenService tokenService)
        {
            this._tokenService = tokenService;
            this._context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserExist(register.Username.ToLower()))
            {
                return BadRequest("Username already exists");
            }

            using HMAC hmac = new HMACSHA512();

            var appUser = new ApplicationUser
            {
                Username = register.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };

            await _context.AppUser.AddAsync(appUser);
            await _context.SaveChangesAsync();

            return new UserDto()
            {
                Username = register.Username,
                Token = _tokenService.CreateToken(appUser)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.AppUser.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }
            using HMAC hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid username or password");
                }
            }
            return new UserDto()
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.AppUser.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}