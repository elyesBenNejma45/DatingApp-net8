namespace API.interfaces;

using API.Entities;

public interface ITokenService
{
    string CreateToken(AppUser appUser);
    
}