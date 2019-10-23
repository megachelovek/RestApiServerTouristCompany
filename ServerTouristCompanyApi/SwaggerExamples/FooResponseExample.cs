using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class FooResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Foo
            {
                Id = new Random().Next(),
                Value = Guid.NewGuid().ToString().Remove(6)
            };
        }
    }
}