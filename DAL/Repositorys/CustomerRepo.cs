using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositorys
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly Context db;

        public CustomerRepo(Context context)
        {
            db = context;
        }

        public async Task Add(tb_customer entity)
        {
            await db.tb_customer.AddAsync(entity);
        }

        public async Task<List<tb_customer>> GetCustomer()
        {
            return await db.tb_customer.AsNoTracking().ToListAsync();
        }

        public async Task<tb_customer> GetCustomerByID(int customerid)
        {
            return await db.tb_customer.FindAsync(customerid);
        }

        public void Remove(tb_customer entity)
        {
            db.tb_customer.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
