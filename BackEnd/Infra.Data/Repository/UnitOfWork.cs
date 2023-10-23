using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatProjectApplicationDBContext _ctx;
        public IRepository<Role> RoleRepository => new BaseRepository<Role>(_ctx);
        public IRepository<User> UserRepository => new BaseRepository<User>(_ctx);
        public IRepository<DataInfo> DataInfoRepository => new BaseRepository<DataInfo>(_ctx);
        public IRepository<History> HistoryRepository => new BaseRepository<History>(_ctx);
        public IRepository<UserHistory> UserHistoryRepository => new BaseRepository<UserHistory>(_ctx);

        public UnitOfWork(ChatProjectApplicationDBContext ctx)
        {
            _ctx = ctx;

        }

        public string GetDbConnection()
        {
            return _ctx.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
