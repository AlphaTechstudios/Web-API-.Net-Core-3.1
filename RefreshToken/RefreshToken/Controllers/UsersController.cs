using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RefreshToken.Models;
using RefreshToken.Models.Dtos;
using RefreshToken.Models.Entities;

namespace RefreshToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager usersManager;
        private readonly IRefreshTokensManager refreshTokensManager;
        private readonly AppSettings appSettings;

        public UsersController(IUsersManager usersManager,
                               IOptions<AppSettings> appSettings,
                               IRefreshTokensManager refreshTokensManager)
        {
            this.usersManager = usersManager;
            this.refreshTokensManager = refreshTokensManager;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(usersManager.GetAllUsers());
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] UserModel userModel)
        {
            var userID = usersManager.Insert(userModel);
            return Ok(userID);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            UserModel userModel = usersManager.Login(loginDto);
            if (userModel == null)
            {
                return BadRequest("Bad login or password.");
            }

            // JWT Tokens
            var now = DateTime.UtcNow;
            var refreshTokenModel = refreshTokensManager.GetToken(userModel.Email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Email, userModel.Email)
                }),
                Expires = now.AddMinutes(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            userModel.Token = tokenHandler.WriteToken(token);

            if(refreshTokenModel != null)
            {
                refreshTokenModel = refreshTokensManager.UpdateToken(refreshTokenModel.RefreshToken);
            }
            else
            {
                refreshTokenModel = refreshTokensManager.AddToken(userModel.Email);
            }

            userModel.RefreshToken = refreshTokenModel.RefreshToken;
            userModel.TokenExpires = now.AddMinutes(4);

            return Ok(userModel);
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            var refreshTokenModel = refreshTokensManager.GetToken(refreshToken);
            var userModel = usersManager.GetUserByEmail(refreshTokenModel.Email);
            DateTime now = DateTime.UtcNow;

            if (refreshTokenModel == null || userModel == null)
            {
                return BadRequest("Bad Token.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Email, userModel.Email)
                }),
                Expires = now.AddMinutes(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            userModel.Token = tokenHandler.WriteToken(token);
            refreshTokenModel = refreshTokensManager.UpdateToken(refreshTokenModel.RefreshToken);
            userModel.RefreshToken = refreshTokenModel.RefreshToken;
            userModel.TokenExpires = now.AddMinutes(4);

            return Ok(userModel);
        }
    }
}
