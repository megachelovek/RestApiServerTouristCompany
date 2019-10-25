using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServerTouristCompanyApi.Models;
using ServerTouristCompanyApi.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.Controllers
{
    public class AccountController : Controller
    {
        private List<Person> people = new List<Person>
        {
            new Person {Login="admin", Password="admin", Role = "admin" },
            new Person { Login="qwerty", Password="55555", Role = "user" }
        };

        /// <summary>
        /// Для получения в !!Body!! надо добавить username:<имя_пользователя> password:<пароль> 
        /// После получение токена access_token, его надо прибавить к 'Bearer '+<token> т.е. в Headers -> Authorization:Bearer eyJhbGciOiJIUzI1NiIsIw....
        /// </summary>
        [HttpPost("/token")]
        [ProducesResponseType(typeof(IEnumerable<AuthResponse>), 200)]
        [SwaggerRequestExample(typeof(Tour), typeof(AuthExample))]
        [ProducesResponseType(500)]
        public async Task Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            List<Claim> list = (List<Claim>) identity.Claims;
            var response = new
            {
                access_token = encodedJwt,
                user = identity.Name,
                role = list[1].Value
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
