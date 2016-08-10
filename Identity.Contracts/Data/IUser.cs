using System;

namespace Identity.Contracts.Data
{
    public interface IUser
    {
        string Username { get; }
        string HashedPassword { get; }
        string Email { get; }
        string Mobile { get; }
        Guid Key { get; }
        void SetKey(Guid key);
    }
}