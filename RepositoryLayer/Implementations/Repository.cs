using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                //_context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            var Data = _dbSet.ToList();
            return Data;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
                //_context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            //_context.SaveChanges();
        }

        //public void Save() => _context.SaveChanges();
    }
}
