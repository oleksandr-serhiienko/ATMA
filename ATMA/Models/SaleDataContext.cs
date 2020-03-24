using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMA.Models
{
    public class SaleDataContext : DbContext
    {
        public SaleDataContext(DbContextOptions<SaleDataContext> options) 
            : base (options)
        {
        }

        public DbSet<SaleData> SaleDatas { get; set; }

    }
}
