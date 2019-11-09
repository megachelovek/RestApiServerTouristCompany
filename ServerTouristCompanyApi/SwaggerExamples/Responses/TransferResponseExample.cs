using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TransferResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Transfer
            {
                Id = 1,
                DateTimeNearTransfer = new DateTime(2019, 8, 1, 6, 20, 0),
                Ticket = new Ticket(DateTime.Now, DateTime.Now, 1, "qwe", "asd", "123"),
                PlaceNearTransfer = "Аэропорт имени Аски из Евангелиона"
            };
        }
    }
}