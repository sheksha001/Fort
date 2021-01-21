using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fort.Models;


namespace Fort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public UserController(IAuthRepository authRepository)
        {
            _authRepo = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User request)
        {
            if (ModelState.IsValid)
            {
                ServiceResponse<int> response = await _authRepo.Register(new User { Name = request.Name, EmailAddress = request.EmailAddress, Password = request.Password });
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            if (ModelState.IsValid)
            {
                ServiceResponse<string> response = await _authRepo.Login(request.EmailAddress, request.Password);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
