using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservetionDesktopApp.DbContexts
{
    public class ReservetionDesktopAppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservetionDesktopAppDbContext>
    {
        public ReservetionDesktopAppDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=ReservetionDesktopApp.db").Options;

            return new ReservetionDesktopAppDbContext(options);
        }
    }
}
