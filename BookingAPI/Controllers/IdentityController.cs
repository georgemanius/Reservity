using BookingAPI.Models.Responses;
using BookingAPI.Models.ViewModels;
using BookingAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private IIdentityRepository _identityRepository;
        public UserManager<IdentityUser> _userManager;

        public IdentityController(IIdentityRepository identityRepository, UserManager<IdentityUser> userManager)
        {
            _identityRepository = identityRepository;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationViewModel request)
        {
            var authResponse = await _identityRepository.RegisterAsync(request);
            if (!authResponse.isSuccessful)
            {
                return Unauthorized(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel request)
        {
            var authResponse = await _identityRepository.LoginAsync(request);
            if (!authResponse.isSuccessful)
            {
                return Unauthorized(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
