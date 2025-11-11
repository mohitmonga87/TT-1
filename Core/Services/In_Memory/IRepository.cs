using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.In_Memory
{
    public interface IRepository<T>
    {
        T Create(T entity);
        T Update(T entity);
        bool Delete(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll(Func<T, bool> filter = null); 
    }
}
