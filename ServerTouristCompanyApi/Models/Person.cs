using System;

namespace ServerTouristCompanyApi.Models
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string login, string password, string role, string name, string address, string phone, DateTime registrationTime)
        {
            Login = login;
            Password = password;
            Role = role;
            Name = name;
            Address = address;
            Phone = phone;
            RegistrationTime = registrationTime;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}