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
                new Ticket(new DateTime(2019, 8, 1, 6, 20, 0), new DateTime(2019, 8, 2, 4, 20, 0), 1, "Аэропорт Курумоч", "Аэропорт имени Аски", "123"),
                new Ticket(new DateTime(2019, 8, 1, 6, 20, 0), new DateTime(2019, 8, 2, 4, 20, 0), 1, "Аэропорт имени Фуу", "Аэропорт имени Аски", "123"),
                new Ticket(new DateTime(2019, 8, 1, 6, 20, 0), new DateTime(2019, 8, 2, 4, 20, 0), 1, "Вокзал имени Спайка", "Вокзал Самара", "123")
            };
        }
    }
}