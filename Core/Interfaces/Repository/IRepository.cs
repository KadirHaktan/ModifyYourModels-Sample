using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface IRepository<T> where T:class
    {
        
        Task<T> CreateAsync(T entity);

        T Create(T entity);

        Task CreateVoidAsync(T entity);

        void CreateVoid(T entity);

    }
}
