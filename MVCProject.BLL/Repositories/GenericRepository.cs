using Microsoft.EntityFrameworkCore;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Data;
using MVCProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Repositories
{
    public class GenericRepository<T> : IGennericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(T entity)
        {
             _dbContext.Add(entity);
            return _dbContext.SaveChanges();
            
        }

        public int Delete(T entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dbContext.Employees.Include(E => E.Department).ToList();
            }
             return _dbContext.Set<T>().ToList();

        }
        public int Update(T entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
