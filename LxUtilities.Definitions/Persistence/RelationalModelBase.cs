namespace LxUtilities.Definitions.Persistence
{
    public abstract class RelationalModelBase : IRelationalModel
    {
        public long Id { get; protected set; }

        public virtual void SetId(long id)
        {
            Id = id;
        }
    }
}