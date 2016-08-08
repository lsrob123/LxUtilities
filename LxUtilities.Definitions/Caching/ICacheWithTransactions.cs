using System;
using System.Threading.Tasks;

namespace LxUtilities.Definitions.Caching
{
    public interface ICacheWithTransactions : ICacheWithHashes
    {
        bool ExecuteTransaction(Func<ICacheWithHashes, Task> transactedOperations);
    }
}