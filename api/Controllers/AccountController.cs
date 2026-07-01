using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{[Route("api/account")]
[ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager; 
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };
                var createUser = await _userManager.CreateAsync(appUser, model.Password);
                if (createUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(appUser, "User");
                    if (addRole.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                    else
                    {
                        return BadRequest(addRole.Errors);
                    }
                }
                else
                {
                    return BadRequest(createUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }   
    }
}