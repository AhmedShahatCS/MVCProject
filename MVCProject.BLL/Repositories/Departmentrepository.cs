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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDbContext _dbContext;//Null

        public DepartmentRepository(ApplicationDbContext DbContext)
        {
            //_dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
            _dbContext = DbContext;
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            //var Result = _dbContext.Departments.FirstOrDefault(D => D.Id == id);
            //return Result;

            //var Result = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
            //if (Result == null)
            //{
            //    Result = _dbContext.Departments.FirstOrDefault(D => D.Id == id);
            //}
            //return Result;

            return _dbContext.Departments.Find(id);

        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.AsNoTracking().ToList();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
