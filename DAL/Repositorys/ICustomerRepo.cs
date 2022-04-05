using System.Threading.Tasks;
using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repositorys
{
    public interface ICustomerRepo
    {
        Task Add(tb_customer entity);
        void Remove(tb_customer entity);
        Task<tb_customer> GetCustomerByID(int customerid);
        Task<List<tb_customer>> GetCustomer();
        Task SaveAsync();
    }
}
