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
                    Id = 1, DateTimeMeet = new DateTime(2019, 8, 1, 6, 20, 0), IdTicket = 1,
                    PlaceMeet = "Аэропорт имени Аски из Евангелиона"
                },

                new Transfer
                {
                    Id = 2, DateTimeMeet = new DateTime(2019, 8, 1, 8, 20, 0), IdTicket = 2,
                    PlaceMeet = "Аэропорт имени Фуу из Самурая Чамплу"
                },

                new Transfer
                {
                    Id = 3, DateTimeMeet = new DateTime(2019, 8, 1, 9, 20, 0), IdTicket = 3,
                    PlaceMeet = "Вокзал имени Спайка из Ковбоя Бибопа"
                }
            };
        }
    }
}