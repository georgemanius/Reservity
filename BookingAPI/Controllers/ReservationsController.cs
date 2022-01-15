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
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationsController : Controller
    {
        private IReservationRepository _reservationRepository;
        private UserManager<IdentityUser> _userManager;
        private readonly IVenueRepository _venueRepository;

        public ReservationsController(IReservationRepository reservationRepository,
                                        UserManager<IdentityUser> userManager,
                                        IVenueRepository venueRepository)
        {
            _reservationRepository = reservationRepository;
            _userManager = userManager;
            _venueRepository = venueRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            return await _reservationRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            var venueToReserve = await _venueRepository.Get(reservation.VenueId);
            if (venueToReserve == null)
            {
                return NotFound(new { message = $"There is no venue with id '{reservation.VenueId}'." });
            }
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            reservation.UserId = currentUser.Id;

            var newReservation = await _reservationRepository.Create(reservation);
            return CreatedAtAction(nameof(GetReservations), new { id = newReservation.ReservationId, newReservation });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return BadRequest(new { message = "Reservation IDs does not match" });
            }

            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            if (currentUser.Id != reservation.UserId)
            {
                return BadRequest(new { message = "It is not the reservation of yours" });
            }

            var initialReservation = await _reservationRepository.Get(id);
            reservation.UserId = initialReservation.UserId;
            reservation.VenueId = initialReservation.VenueId;
            reservation.CreatedDate = initialReservation.CreatedDate;
            
            await _reservationRepository.Update(reservation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservationToDelete = await _reservationRepository.Get(id);
            if (reservationToDelete == null)
            {
                return NotFound();
            }

            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            if (currentUser.Id != reservationToDelete.UserId)
            {
                return BadRequest(new { message = "It is not the reservation of yours" });
            }

            await _reservationRepository.Delete(id);
            return NoContent();
        }
    }
}

