using BookingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetAll();
        Task<Venue> Get(int id);
        Task<Venue> Create(Venue venue);
        Task Update(Venue venue);
        Task Delete(int id);
    }
}
