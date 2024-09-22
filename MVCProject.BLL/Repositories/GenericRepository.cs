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
        public async Task AddAsync(T entity)
        {
           await  _dbContext.AddAsync(entity);
            //return _dbContext.SaveChanges();
            
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
            //return _dbContext.SaveChanges();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).ToListAsync();
            }
             return await _dbContext.Set<T>().ToListAsync();

        }
        public void Update(T entity)
        {
            _dbContext.Update(entity);
            //return _dbContext.SaveChanges();
        }
    }
}
