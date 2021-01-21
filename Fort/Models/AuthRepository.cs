using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace Fort.Models
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FortCodeContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(FortCodeContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        //---> User Register 
        public async Task<ServiceResponse<int>> Register(User user)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExists(user.EmailAddress))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            user.Password = Utility.Encryptdata(user.Password.ToString()); ;
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user.UserId;
            return response;
        }
        public async Task<bool> UserExists(string usermail)
        {
            if (await _context.Users.AnyAsync(x => x.EmailAddress.ToLower() == usermail.ToLower()))
            {
                return true;
            }
            return false;
        }

        //---> User Login 
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User Mail Address not found.";
            }
            else if (!VerifyPasswordHash(password, user.Password))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
              response.Data = CreateToken(user);
            }
            return response;
        }
        private bool VerifyPasswordHash(string password, string DBpassword)
        {
            string EncrPassword = Utility.Encryptdata(password);
            if (EncrPassword == DBpassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string CreateToken(User user)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.EmailAddress)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
