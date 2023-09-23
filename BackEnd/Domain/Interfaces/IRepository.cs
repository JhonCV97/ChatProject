using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<List<T>> AddRange(List<T> entity);
        Task<T> Put(T entity);
        Task<bool> Delete(T entity);
    }
}
