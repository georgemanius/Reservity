using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private AppDbContext _ctx; 

        public ReservationRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Reservation> Create(Reservation reservation)
        {
            
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();
            return reservation;
        }

        public async Task Delete(int id)
        {
            var reservationToDelete = await _ctx.Reservations.FindAsync(id);
            _ctx.Reservations.Remove(reservationToDelete);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Reservation> Get(int id)
        {
            return await _ctx.Reservations.FindAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _ctx.Reservations.ToListAsync();
        }

        public async Task Update(Reservation reservation)
        {
            _ctx.Entry(reservation).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}
