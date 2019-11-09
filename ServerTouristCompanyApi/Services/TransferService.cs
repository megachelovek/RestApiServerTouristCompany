using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    internal class TransferService : ITransferService
    {
        public Task<int> Create(Transfer Transfer)
        {
            return Task.FromResult(new Random().Next());
        }

        public Task<IEnumerable<Transfer>> Get()
        {
            return Task.FromResult<IEnumerable<Transfer>>(new List<Transfer>
            {
                new Transfer
                {
                    Id = 1, DateTimeNearTransfer = new DateTime(2019, 8, 1, 6, 20, 0), Ticket = new Ticket(DateTime.Now, DateTime.Now, 1, "qwe", "asd", "123"),
                    PlaceNearTransfer = "Аэропорт имени Аски из Евангелиона"
                },

                new Transfer
                {
                    Id = 2, DateTimeNearTransfer = new DateTime(2019, 8, 1, 8, 20, 0), Ticket = new Ticket(DateTime.Now, DateTime.Now, 2, "qwe", "asd", "123"),
                    PlaceNearTransfer = "Аэропорт имени Фуу из Самурая Чамплу"
                },

                new Transfer
                {
                    Id = 3, DateTimeNearTransfer = new DateTime(2019, 8, 1, 9, 20, 0), Ticket = new Ticket(DateTime.Now, DateTime.Now, 3, "qwe", "asd", "123"),
                    PlaceNearTransfer = "Вокзал имени Спайка из Ковбоя Бибопа"
                }
            });
        }

        public Task<Transfer> Get(int id)
        {
            if (id == 0)
                return Task.FromResult<Transfer>(null);

            return Task.FromResult(new Transfer
            {
                Id = 2, DateTimeNearTransfer = new DateTime(2019, 8, 1, 8, 20, 0),
                Ticket = new Ticket(DateTime.Now, DateTime.Now, 3, "qwe", "asd", "123"),
                PlaceNearTransfer = "Аэропорт имени Фуу из Самурая Чамплу"
            });
        }

        public Task Update(Transfer Transfer)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
    }
}