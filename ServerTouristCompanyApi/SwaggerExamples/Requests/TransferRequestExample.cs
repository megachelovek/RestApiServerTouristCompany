using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TransferRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Transfer
            {
                Id = 3, DateTimeNearTransfer = new DateTime(2019, 8, 1, 9, 20, 0),
                Ticket = new Ticket(DateTime.Now, DateTime.Now, 3, "qwe", "asd", "123"),
                PlaceNearTransfer = "Вокзал имени Спайка из Ковбоя Бибопа"
            };
        }
    }
}