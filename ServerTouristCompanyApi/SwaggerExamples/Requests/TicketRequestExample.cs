using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TicketRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Ticket
            {
                Id = 1, DateTimeDeparture = new DateTime(2019, 8, 1, 6, 20, 0),
                DateTimeArrival = new DateTime(2019, 8, 2, 4, 20, 0), PlaceArrival = "Аэропорт имени Аски",
                PlaceDeparture = "Аэропорт Курумоч"
            };
        }
    }
}