using MyPortFolio.Entities;

namespace MyPortFolio.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
