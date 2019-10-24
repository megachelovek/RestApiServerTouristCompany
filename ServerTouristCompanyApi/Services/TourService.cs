using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Controllers;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    internal class TourService : ITourService
    {
        public Task<int> Create(Tour Tour)
        {
            return Task.FromResult(new Random().Next());
        }

        public Task<IEnumerable<Tour>> Get()
        {
            return Task.FromResult<IEnumerable<Tour>>(new List<Tour>
            {
                new Tour { Id = 1,Name = "Морской тур в Италию",Create = new DateTime(2019,9,8),Description = "Тур по городам Италии с заездом во все места с достопримечательностями",FromCity = "Санкт-Петербург, Россия",Price = 90000, ToCity = "Рим, Милан, Италия"},

                new Tour { Id = 2,Name = "Пеший тур в Болгарию",Create = new DateTime(2019,3,3),Description = "Тур по городам Болгарии ",FromCity = "Москва, Россия",Price = 20000, ToCity = "София, Болгария"},

                new Tour { Id = 3,Name = "Аниме тур в Японию",Create = new DateTime(2019,6,7),Description = "Объезд всех аниме-студий Токио",FromCity = "Москва, Россия",Price = 120000, ToCity = "Токио, Япония"},
            });
        }

        public Task<Tour> Get(int id)
        {
            if (id == 0)
                return Task.FromResult<Tour>(null);

            return Task.FromResult(new Tour { Id = 2, Name = "Пеший тур в Болгарию", Create = new DateTime(2019, 3, 3), Description = "Тур по городам Болгарии ", FromCity = "Москва, Россия", Price = 20000, ToCity = "София, Болгария" });
        }

        public Task Update(Tour Tour)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
    }
}