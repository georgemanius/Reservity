using BookingAPI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Delete(string id)
        {
            var userToDelete = _userManager.Users.FirstOrDefault(u => u.Id == id);
            await _userManager.DeleteAsync(userToDelete); 
        }

        public IdentityUser Get(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return _userManager.Users.ToList();
        }
    }
}