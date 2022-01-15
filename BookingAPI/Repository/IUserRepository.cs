using BookingAPI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public interface IUserRepository
    {
        IEnumerable<IdentityUser> GetAll();
        IdentityUser Get(string id);
        Task Delete(string id);

    }
}
