using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly ChatProjectApplicationDBContext _ctx;
        private DbSet<T> _entities;
        public BaseRepository(ChatProjectApplicationDBContext ctx)
        {
            _ctx = ctx;
            _entities = ctx.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _entities;
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                _entities.Add(entity);
                var test = await _ctx.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> AddRange(List<T> entity)
        {
            _entities.AddRange(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Put(T entity)
        {
            _entities.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> Delete(T entity)
        {
            _entities.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
