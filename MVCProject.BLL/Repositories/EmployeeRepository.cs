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
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext db) : base(db) { }

    }
}
