using Internship2022WebAPI.Data.DaoModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Internship2022WebAPI.Data
{
    public class RetailDbContext : DbContext
    {
        public RetailDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        public DbSet<ProductDao> Products { get; set; }
    }
}
