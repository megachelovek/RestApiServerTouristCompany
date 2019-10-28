using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    internal class ReservationService : IReservationService
    {
        public Task<int> Create(Reservation Reservation)
        {
            return Task.FromResult(new Random().Next());
        }

        public Task<IEnumerable<Reservation>> Get()
        {
            return Task.FromResult<IEnumerable<Reservation>>(new List<Reservation>
            {
                new Reservation
                    {Id = 1, Name = "Отель Season", NameContact = "Ресепшен отеля", NumberContact = 79202131232},

                new Reservation
                    {Id = 2, Name = "Гостиница Волга", NameContact = "Ресепшен отеля", NumberContact = 79202345532},

                new Reservation
                    {Id = 3, Name = "Отель Hilton", NameContact = "Ресепшен отеля", NumberContact = 792021345672}
            });
        }

        public Task<Reservation> Get(int id)
        {
            if (id == 0)
                return Task.FromResult<Reservation>(null);

            return Task.FromResult(new Reservation
                {Id = 2, Name = "Гостиница Волга", NameContact = "Ресепшен отеля", NumberContact = 79202345532});
        }

        public Task Update(Reservation Reservation)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
    }
}