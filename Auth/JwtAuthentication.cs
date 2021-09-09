using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net5.Sample.API.Auth
{
    public class JwtAuthentication : IAuth
    {
        private readonly string _encryptionKey;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public JwtAuthentication(IConfiguration p_configuration)
        {
            _encryptionKey = p_configuration["ApiSettings:ApiEncryption"];
            _apiKey = p_configuration["ApiSettings:ApiKey"];
            _apiSecret = p_configuration["ApiSettings:ApiSecret"];
        }

        public string Authenticate(string p_apiKey, string p_apiSecret)
        {
            if(string.IsNullOrWhiteSpace(p_apiSecret) || string.IsNullOrWhiteSpace(p_apiKey))
            {
                return null;
            }

            if(!_apiSecret.Equals(p_apiSecret) || !_apiKey.Equals(p_apiKey))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_encryptionKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, _apiKey) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
