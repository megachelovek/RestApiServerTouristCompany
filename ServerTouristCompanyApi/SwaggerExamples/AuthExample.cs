using System;
using ServerTouristCompanyApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.SwaggerExamples
{
    public class AuthExample:IExamplesProvider
    {
        /// <summary>
        /// Для получения в !!Body!! надо добавить username:<имя_пользователя> password:<пароль> 
        /// В access_token будет string его надо прибавить к 'Bearer '+<token>
        /// т.е. в Headers
        /// Authorization:Bearer eyJhbGciOiJIUzI1NiIsIw....
        /// </summary>
        public object GetExamples()
        {
            return new AuthResponse
            {
                access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicXdlcnR5IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTU3MTkwNDI0NiwiZXhwIjoxNTcxOTA3MTg2LCJpc3MiOiJBdXRoU2VydmVyIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1Mjc0NC8ifQ.7I9mvjEpYS4J_EDMWdeVRFeDbHZGozir18rizvNwolU"  ,
                user = "qwerty"
            };
        }
    }
}
