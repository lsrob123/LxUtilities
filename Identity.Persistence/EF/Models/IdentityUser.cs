using Identity.Domain.Entities;
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