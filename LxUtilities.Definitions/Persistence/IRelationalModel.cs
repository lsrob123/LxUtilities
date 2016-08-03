namespace LxUtilities.Definitions.Persistence
{
    public interface IRelationalModel
    {
        long Id { get; }
        void SetId(long id);
    }
}