using BookingAPI.Models.ViewModels;
using BookingAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUser(string id)
        {
            var user = _userRepository.Get(id);
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> GetUsers()
        {
            var users = _userRepository.GetAll();
            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            }); 
            
            return userViewModels.ToList();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserViewModel>> DeleteUser(string id)
        {
            var userToDelete = _userRepository.Get(id);
            if(userToDelete == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
