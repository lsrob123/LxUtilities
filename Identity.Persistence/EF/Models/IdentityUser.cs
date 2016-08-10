using Identity.Domain;
using LxUtilities.Definitions.Persistence;

namespace Identity.Persistence.EF.Models
{
    public class IdentityUser : GenericRelationalModel<User>
    {
        public IdentityUser()
        {
        }

        public IdentityUser(User user) : base(user)
        {
        }
    }
}