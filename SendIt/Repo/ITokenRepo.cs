using Microsoft.AspNetCore.Identity;

namespace SendIt.Repo
{
    public interface ITokenRepo
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
