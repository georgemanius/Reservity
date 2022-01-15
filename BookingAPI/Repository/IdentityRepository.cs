using BookingAPI.Models.Responses;
using BookingAPI.Models.ViewModels;
using BookingAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private UserManager<IdentityUser> _userManager;
        private  JwtSettings _jwtSettings { get; set; }
        public IdentityRepository(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public AuthenticationResult GenerateAuthenticationResultForUser(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                isSuccessful = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
        public async Task<AuthenticationResult> RegisterAsync(RegistrationViewModel vm)
        {
            var user = await _userManager.FindByNameAsync(vm.Username);
            var existingEmail = await _userManager.FindByEmailAsync(vm.Email);

            if (user != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {$"User with the username '{vm.Username}' already exists."}
                };
            }

            if (existingEmail != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"User with the email '{vm.Email}' already exists." }
                };
            }

            var newUser = new IdentityUser
            {
                UserName = vm.Username,
                Email = vm.Email
            };
            
            var createdUser = await _userManager.CreateAsync(newUser, vm.Password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(e => e.Description)
                };
            }
            await _userManager.AddToRoleAsync(newUser, "Customer");

            return GenerateAuthenticationResultForUser(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginViewModel vm)
        {
            var user = await _userManager.FindByNameAsync(vm.Username);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"User does not exist." }
                };
            }

            bool userHasValidPassword = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"User/password combination is not valid." }
                };
            }
            bool isCustomer = await _userManager.IsInRoleAsync(user, "Customer");
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            return GenerateAuthenticationResultForUser(user);
        }
    }
}
