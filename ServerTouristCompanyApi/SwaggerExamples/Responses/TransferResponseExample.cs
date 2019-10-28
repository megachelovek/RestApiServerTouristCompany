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
                Id = 1, DateTimeMeet = new DateTime(2019, 8, 1, 6, 20, 0), IdTicket = 1,
                PlaceMeet = "Аэропорт имени Аски из Евангелиона"
            };
        }
    }
}