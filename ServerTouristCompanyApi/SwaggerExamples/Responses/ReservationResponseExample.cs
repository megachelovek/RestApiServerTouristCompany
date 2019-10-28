using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class ReservationResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Reservation
                {Id = 1, Name = "Отель Season", NameContact = "Ресепшен отеля", NumberContact = 79202131232};
        }
    }
}