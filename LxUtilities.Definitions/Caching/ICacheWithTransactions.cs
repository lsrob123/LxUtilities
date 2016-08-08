using System;

namespace LxUtilities.Definitions.Caching
{
    public interface ICacheWithTransactions : ICacheWithHashes
    {
        bool ExecuteTransaction(Action<ICacheWithHashes> transactedOperations);
    }
}