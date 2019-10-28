namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    ///     В access_token будет string его надо прибавить к 'Bearer '+
    ///     <token> т.е. в Headers -> Authorization:Bearer eyJhbGciOiJIUzI1NiIsIw....
    /// </summary>
    public class AuthResponse
    {
        public string access_token { get; set; }
        public string user { get; set; }
    }
}