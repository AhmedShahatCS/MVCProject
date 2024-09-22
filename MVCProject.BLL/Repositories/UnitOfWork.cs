using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext Db)
        {
            EmpRepo = new EmployeeRepository(Db);
            DeptRepo = new DepartmentRepository(Db);

            _db = Db;
        }
        public IEmployeeRepository EmpRepo { get ; set ; }
        public IDepartmentRepository DeptRepo { get; set ; }

        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
