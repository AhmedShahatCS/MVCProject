using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Interfaces
{
    public interface IGennericRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
