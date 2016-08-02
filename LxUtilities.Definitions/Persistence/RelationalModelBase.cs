using LxUtilities.Definitions.Domain;
using LxUtilities.Definitions.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public class RelationalModelBase : EntityBase, IRelationalModel
    {
        public long Id { get; protected set; }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}