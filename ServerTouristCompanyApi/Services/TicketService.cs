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
                new Ticket(new DateTime(2019, 8, 1, 6, 20, 0), new DateTime(2019, 8, 2, 4, 20, 0), 1, "Аэропорт Курумоч", "Аэропорт имени Аски", "123"),
                new Ticket(new DateTime(2019, 8, 1, 6, 20, 0), new DateTime(2019, 8, 2, 4, 20, 0), 1, "Аэропорт имени Фуу", "Аэропорт имени Аски", "123"),
                new Ticket(new DateTime(2019, 8, 1, 6, 20, 0), new DateTime(2019, 8, 2, 4, 20, 0), 1, "Вокзал имени Спайка", "Вокзал Самара", "123")
            });
        }

        public Task<Ticket> Get(int id)
        {
            if (id == 0)
                return Task.FromResult<Ticket>(null);

            return Task.FromResult(new Ticket(
                new DateTime(2019, 8, 9, 2, 20, 0),
                new DateTime(2019, 8, 10, 1, 20, 0), 2, "Аэропорт имени Фуу",
                "Аэропорт имени Аски", "123")
            );
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