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
                Id = 3, DateTimeMeet = new DateTime(2019, 8, 1, 9, 20, 0), IdTicket = 3,
                PlaceMeet = "Вокзал имени Спайка из Ковбоя Бибопа"
            };
        }
    }
}