using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.API.DTOs;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.unitOfWork;
using Microsoft.EntityFrameworkCore;
using WheelsCrawler.API.Interfaces;

namespace WheelsCrawler.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly ITokenService _tokenService;

        public AccountController(IUnitOfWork unityOfWork, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _unityOfWork = unityOfWork;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDTO)
        {
            if (await UserExist(registerDTO.Username))
                return BadRequest("username already exist");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            await _unityOfWork.Repository<User>().CreateAsync(user);
            if (await _unityOfWork.Repository<User>().SaveAll())
            {
                return new UserDTO
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };
            }
            return BadRequest("there are errors, man");

        }

        private async Task<bool> UserExist(string username)
        {
            return await _unityOfWork.Repository<User>().GetAll()
                .AnyAsync(x => x.UserName == username);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _unityOfWork.Repository<User>().GetAll().SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("invalid password");
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }

    }
}