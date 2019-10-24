using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TourRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Tour
            {
                Id = 2, Name = "Пеший тур в Болгарию", Create = new DateTime(2019, 3, 3),
                Description = "Тур по городам Болгарии ", FromCity = "Москва, Россия", Price = 20000,
                ToCity = "София, Болгария"
            };
        }
    }
}