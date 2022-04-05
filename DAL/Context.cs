using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<tb_customer> tb_customer { get; set; }
    }
}
