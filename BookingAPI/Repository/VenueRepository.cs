using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public class VenueRepository : IVenueRepository
    {
        private AppDbContext _ctx;

        public VenueRepository(AppDbContext ctx)
        {
            _ctx = ctx;
         
        }
        public async Task<Venue> Create(Venue venue)
        {
            _ctx.Venues.Add(venue);
           await _ctx.SaveChangesAsync();
            return venue;
        }

        public async Task Delete(int id)
        {
            var venueToDelete = await _ctx.Venues.FindAsync(id);
            _ctx.Venues.Remove(venueToDelete);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Venue>> GetAll()
        {
            return await _ctx.Venues.ToListAsync();
        }

        public async Task<Venue> Get(int id)
        {
            return await _ctx.Venues.FindAsync(id);
        }

        public async Task Update(Venue venue)
        {
            _ctx.Entry(venue).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}
