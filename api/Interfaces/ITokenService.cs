using api.Entities;

namespace api.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUser user);
    }
}