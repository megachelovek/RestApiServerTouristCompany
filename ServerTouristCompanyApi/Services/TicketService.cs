using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    internal class TicketService : ITicketService
    {
        public Task<int> Create(Ticket Ticket)
        {
            return Task.FromResult(new Random().Next());
        }

        public Task<IEnumerable<Ticket>> Get()
        {
            return Task.FromResult<IEnumerable<Ticket>>(new List<Ticket>
            {
                new Ticket
                {
                    Id = 1, DateTimeDeparture = new DateTime(2019, 8, 1, 6, 20, 0),
                    DateTimeArrival = new DateTime(2019, 8, 2, 4, 20, 0), PlaceArrival = "Аэропорт имени Аски",
                    PlaceDeparture = "Аэропорт Курумоч"
                },

                new Ticket
                {
                    Id = 2, DateTimeDeparture = new DateTime(2019, 8, 9, 2, 20, 0),
                    DateTimeArrival = new DateTime(2019, 8, 10, 1, 20, 0), PlaceArrival = "Аэропорт имени Фуу",
                    PlaceDeparture = "Аэропорт имени Аски"
                },

                new Ticket
                {
                    Id = 3, DateTimeDeparture = new DateTime(2019, 8, 4, 6, 20, 0),
                    DateTimeArrival = new DateTime(2019, 8, 6, 2, 20, 0), PlaceArrival = "Вокзал имени Спайка",
                    PlaceDeparture = "Вокзал Самара"
                }
            });
        }

        public Task<Ticket> Get(int id)
        {
            if (id == 0)
                return Task.FromResult<Ticket>(null);

            return Task.FromResult(new Ticket
            {
                Id = 2, DateTimeDeparture = new DateTime(2019, 8, 9, 2, 20, 0),
                DateTimeArrival = new DateTime(2019, 8, 10, 1, 20, 0), PlaceArrival = "Аэропорт имени Фуу",
                PlaceDeparture = "Аэропорт имени Аски"
            });
        }

        public Task Update(Ticket Ticket)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
    }
}