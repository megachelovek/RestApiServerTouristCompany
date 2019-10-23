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
                new Tour { Id = new Random().Next(), Value = Guid.NewGuid().ToString().Remove(6)},
                new Tour { Id = new Random().Next(), Value = Guid.NewGuid().ToString().Remove(6)}
            };
        }
    }
}