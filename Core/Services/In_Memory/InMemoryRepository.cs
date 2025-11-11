using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services.In_Memory
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _storage =  new List<T>();

        public T Create(T entity)
        {
            _storage.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            if (prop == null) throw new Exception("Entity must have Id property");

            var id = (Guid)prop.GetValue(entity);
            var existing = _storage.FirstOrDefault(x => (Guid)prop.GetValue(x) == id);
            if (existing == null) return null;

            _storage.Remove(existing);
            _storage.Add(entity);
            return entity;
        }

        public bool Delete(Guid id)
        {
            var prop = typeof(T).GetProperty("Id");
            var existing = _storage.FirstOrDefault(x => (Guid)prop.GetValue(x) == id);
            if (existing == null) return false;
            _storage.Remove(existing);
            return true;
        }

        public T GetById(Guid id)
        {
            var prop = typeof(T).GetProperty("Id");
            return _storage.FirstOrDefault(x => (Guid)prop.GetValue(x) == id);
        }

        public IEnumerable<T> GetAll(Func<T, bool> filter = null)
        {
            return filter == null ? _storage : _storage.Where(filter);
        }
    }
}
