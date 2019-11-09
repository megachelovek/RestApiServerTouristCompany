using System;
using System.Collections.Generic;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TransferListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<Transfer>
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
            };
        }
    }
}