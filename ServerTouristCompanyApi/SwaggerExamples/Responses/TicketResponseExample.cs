using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TicketResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Ticket
            {
                Id = 2, DateTimeDeparture = new DateTime(2019, 8, 9, 2, 20, 0),
                DateTimeArrival = new DateTime(2019, 8, 10, 1, 20, 0), PlaceArrival = "Аэропорт имени Фуу",
                PlaceDeparture = "Аэропорт имени Аски"
            };
        }
    }
}