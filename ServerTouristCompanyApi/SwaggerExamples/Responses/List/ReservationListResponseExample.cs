using System.Collections.Generic;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class ReservationListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<Reservation>
            {
                new Reservation
                    {Id = 1, Name = "Отель Season", NameContact = "Ресепшен отеля", NumberContact = 79202131232},

                new Reservation
                    {Id = 2, Name = "Гостиница Волга", NameContact = "Ресепшен отеля", NumberContact = 79202345532},

                new Reservation
                    {Id = 3, Name = "Отель Hilton", NameContact = "Ресепшен отеля", NumberContact = 792021345672}
            };
        }
    }
}