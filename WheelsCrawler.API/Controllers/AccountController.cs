using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.unitOfWork;
using Microsoft.EntityFrameworkCore;
using WheelsCrawler.API.Interfaces;
using System.Collections.Generic;
using WheelsCrawler.Data.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WheelsCrawler.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUnitOfWork _uof;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IUnitOfWork uof, ITokenService tokenService, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _uof = uof;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDTO)
        {

            if (await UserExist(registerDTO.Username))
                return BadRequest("username already exist");

            var user = _mapper.Map<User>(registerDTO);


            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users
                .AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

        }

        [Authorize]
        [HttpGet("users/{id}")]
        public async Task<ActionResult<MemberDTO>> GetUser(int id)
        {
            var member = await _uof.Repository<User>().GetById(id);
            var memberToReturn = _mapper.Map<MemberDTO>(member);
            return memberToReturn;
        }

        [Authorize]
        [HttpGet("Users")]
        public ActionResult<IEnumerable<MemberDTO>> GetUsers()
        {
            var members = _uof.Users.GetAll();
            var membersToRetrun = _mapper.Map<IEnumerable<MemberDTO>>(members);
            return Ok(membersToRetrun);
        }

    }
}