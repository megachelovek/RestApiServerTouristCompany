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
                new Tour {Id = 1, Value = Guid.NewGuid().ToString().Remove(5)},
                new Tour {Id = 3, Value = Guid.NewGuid().ToString().Remove(5)}
            });
        }

        public Task<Tour> Get(int id)
        {
            if (id == 0)
                return Task.FromResult<Tour>(null);

            return Task.FromResult(new Tour { Id = id, Value = Guid.NewGuid().ToString().Remove(5) });
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