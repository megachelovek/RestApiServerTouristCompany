using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TicketResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Ticket(
                new DateTime(2019, 8, 1, 6, 20, 0),
                new DateTime(2019, 8, 2, 4, 20, 0), 2, "Аэропорт имени Аски",
                "Аэропорт Курумоч", "123"
            );
        }
    }
}