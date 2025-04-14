namespace API.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Data.Migrations;
using API.Entities;
using API.interfaces;
using Microsoft.IdentityModel.Tokens;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from AppSettings"); //get tge token key
        if(tokenKey.Length < 64) throw new Exception("your tokenKey needs to be longer"); // tokenKey must be longer than 64

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); //create a new symmetric key

        var claims = new List<Claim> // list of claims to create the token based on user // clai identity
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserName)
        };

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature); // hash the key used by the server
        var tokenDescriptor = new SecurityTokenDescriptor(){

            Subject = new ClaimsIdentity(claims), // token identity
            Expires = DateTime.UtcNow.AddDays(7), // expiring Date
            SigningCredentials = creds // signature of key
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}