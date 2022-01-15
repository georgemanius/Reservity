using BookingAPI.Models.Responses;
using BookingAPI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public interface IIdentityRepository
    {
        Task<AuthenticationResult> RegisterAsync(RegistrationViewModel vm);
        Task<AuthenticationResult> LoginAsync(LoginViewModel vm);
    }
}
