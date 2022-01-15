using BookingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAll();
        Task<Reservation> Get(int id);
        Task<Reservation> Create(Reservation reservation);
        Task Update(Reservation reservation);
        Task Delete(int id);
    }
}
