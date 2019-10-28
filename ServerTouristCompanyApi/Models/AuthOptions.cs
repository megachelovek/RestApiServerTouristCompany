using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ServerTouristCompanyApi.Controllers
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer"; // издатель токена
        public const string AUDIENCE = "http://localhost:52744/"; // потребитель токена
        private const string KEY = "Anime_is_my_life!!111"; // ключ для шифрации
        public const int LIFETIME = 49; // время жизни токена - 1 минута

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}