using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly List<Person> people = new List<Person>(new Person[] { new Person("qwerty", "55555", "user", "name", "address", "phone", new DateTime(2019, 8, 1, 6, 20, 0)), new Person("admin", "admin", "admin", "nameAdmin", "address", "phone", new DateTime(2019, 8, 1, 6, 20, 0)) });

        /// <summary>
        ///     Для получения в !!Body!! надо добавить username:
        ///     <имя_пользователя>
        ///         password:
        ///         <пароль>
        ///             После получение токена access_token, его надо прибавить к 'Bearer '+
        ///             <token> т.е. в Headers -> Authorization:Bearer eyJhbGciOiJIUzI1NiIsIw....
        /// </summary>
        [HttpPost("/token")]
        [ProducesResponseType(typeof(IEnumerable<AuthResponse>), 200)]
        [SwaggerRequestExample(typeof(Tour), typeof(AuthExample))]
        [ProducesResponseType(500)]
        public async Task Token()
        {
            var username = HttpContext.Request.Query["username"].ToString();
            var password = HttpContext.Request.Query["password"].ToString();

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
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var list = (List<Claim>)identity.Claims;
            var response = new
            {
                access_token = encodedJwt,
                user = identity.Name,
                role = list[1].Value
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response,
                new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpGet("/GetAllClients")]
        [ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(IEnumerable<Transfer>), 200)]
        [SwaggerResponseExample(200, typeof(TransferResponseExample))]
        [Authorize(AuthenticationSchemes =
           JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            var persons = people.Where(x => !x.Login.Equals("admin")).Select(x => { x.Password = ""; return x; }).ToList();

            return Ok(persons);
        }

        [HttpGet("/GetClients")]
        [Authorize(AuthenticationSchemes =
           JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetClients(string login)
        {
            var result = people.Where(x => x.Login.Equals(login)).Select(x => { x.Password = ""; return x; }).First();

            return Ok(result);
        }

        [HttpPost("/updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] Person person)
        {
            people.Where(x => x.Login.Equals(person.Login)).ToList().ForEach(x => { x.Name = person.Name; x.Phone = person.Phone; x.Address = person.Address; });
            return Ok(null);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                var claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}