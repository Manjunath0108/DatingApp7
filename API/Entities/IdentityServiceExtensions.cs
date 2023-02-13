using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Entities
{
    public  static class IdentityServiceExtensions
    {
      public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config){
       services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>{
    options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding
        .UTF8.GetBytes(config["TokenKey"])),
        ValidateIssuer=false,
        ValidateAudience=false
    };
}); 
return services;
      }  
    }
}

// adding JWT authentication to an ASP.NET Core application using the IServiceCollection extension method


//The method takes an instance of IConfiguration as a parameter, 
// which is used to retrieve the value of the TokenKey setting from the application's configuration. 
// This value is used as the key for a symmetric security key, which is used to validate the 
// authenticity of the JSON Web Token (JWT) sent with each request to the API.