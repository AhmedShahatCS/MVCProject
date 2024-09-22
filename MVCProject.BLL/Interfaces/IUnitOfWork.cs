using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmpRepo { get; set; }
        public IDepartmentRepository DeptRepo { get; set; }
        public Task<int> CompleteAsync();



    }


}
