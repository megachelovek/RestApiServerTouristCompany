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
using ServerTouristCompanyApi.DAO;
using ServerTouristCompanyApi.Models;
using ServerTouristCompanyApi.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.Controllers
{
    public class AccountController : Controller
    {
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

            var identity = await GetIdentity(username, password);
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
            var persons = await ClientRepository.GetAll();
            persons = persons.Select(x => { x.Password = ""; return x; }).ToList();

            return Ok(persons);
        }

        [HttpGet("/GetClients")]
        [Authorize(AuthenticationSchemes =
           JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetClients(string login)
        {
            var result = await ClientRepository.GetByID(login);
            result.Password = "";

            return Ok(result);
        }

        [HttpPost("/updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] Person person)
        {
            var result = await ClientRepository.Update(person);
            return Ok(result);
        }

        [HttpPost("/registration")]
        public async Task<IActionResult> Registration([FromBody] Person person)
        {
            var result = await ClientRepository.Add(person);
            if(result == 1)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("/deleteClient")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Delete(string login)
        {
            var result = await ClientRepository.Remove(login);

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Ticket), typeof(TicketRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] Person person)
        {
            var result = await ClientRepository.Update(person);
            return Ok(result);
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var person = await ClientRepository.GetByID(username);
            if (person != null && person.Password == password)
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