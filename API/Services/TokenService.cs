using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;

using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
           {
            new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
           };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}



/*
This is a code implementation of a token service in C# that uses JSON Web Token (JWT) 
for authentication. The service uses the JwtSecurityTokenHandler class to generate tokens 
and the SymmetricSecurityKey class to secure the token with a secret key. The secret key is
 passed as a configuration parameter to the service constructor. The service implements the 
 ITokenService interface.

The CreateToken method takes an AppUser object and creates a JWT token with claims based 
on the user's information. The token is signed with HmacSha512 signature using the SigningCredentials 
class and has a lifespan of 7 days. The method returns the generated token as a string.
*/