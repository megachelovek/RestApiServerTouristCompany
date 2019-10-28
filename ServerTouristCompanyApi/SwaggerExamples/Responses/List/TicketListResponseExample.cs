using System;
using System.Collections.Generic;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TicketListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<Ticket>
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
            };
        }
    }
}