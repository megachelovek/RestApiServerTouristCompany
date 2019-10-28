using System;
using System.Collections.Generic;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class TourListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<Tour>
            {
                new Tour
                {
                    Id = 1, Name = "Морской тур в Италию", Create = new DateTime(2019, 9, 8),
                    Description = "Тур по городам Италии с заездом во все места с достопримечательностями",
                    FromCity = "Санкт-Петербург, Россия", Price = 90000, ToCity = "Рим, Милан, Италия"
                },

                new Tour
                {
                    Id = 2, Name = "Пеший тур в Болгарию", Create = new DateTime(2019, 3, 3),
                    Description = "Тур по городам Болгарии ", FromCity = "Москва, Россия", Price = 20000,
                    ToCity = "София, Болгария"
                },

                new Tour
                {
                    Id = 3, Name = "Аниме тур в Японию", Create = new DateTime(2019, 6, 7),
                    Description = "Объезд всех аниме-студий Токио", FromCity = "Москва, Россия", Price = 120000,
                    ToCity = "Токио, Япония"
                }
            };
        }
    }
}