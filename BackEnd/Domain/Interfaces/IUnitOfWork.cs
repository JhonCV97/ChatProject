using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<DataInfo> DataInfoRepository { get; }
        IRepository<History> HistoryRepository { get; }
        IRepository<UserHistory> UserHistoryRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
        string GetDbConnection();
    }
}
