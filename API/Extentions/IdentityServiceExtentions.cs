using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class IdentityServiceExtentions
{
    public static IServiceCollection addIdentityCollection(this IServiceCollection services,
     IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // flags
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        return services;
    }
}