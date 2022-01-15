using BookingAPI.Models;
using BookingAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    
    public class VenuesController : Controller
    {
        private IVenueRepository _venueRepository;
        public VenuesController(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }
       
        [HttpGet]
        public async Task<IEnumerable<Venue>> GetVenues()
        {
           return await _venueRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            return await _venueRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Venue>> CreateVenue(Venue venue)
        {
            var newVenue = await _venueRepository.Create(venue);
            return CreatedAtAction(nameof(GetVenues), new {id = newVenue.VenueId, newVenue});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Venue>> UpdateVenue(int id, Venue venue)
        {
            if(id != venue.VenueId)
            {
                return BadRequest();
            }
            await _venueRepository.Update(venue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Venue>> DeleteVenue(int id)
        {
            var bookToDelete = await _venueRepository.Get(id);
            if(bookToDelete == null)
            {
                return NotFound();
            }
            
            await _venueRepository.Delete(id);
            return NoContent();
        }
    }
}
