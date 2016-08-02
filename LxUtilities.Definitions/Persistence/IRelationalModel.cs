using LxUtilities.Definitions.Domain;

namespace LxUtilities.Definitions.Persistence
{
    public interface IRelationalModel : IEntity
    {
        long Id { get; }

        void SetId(long id);
    }
}