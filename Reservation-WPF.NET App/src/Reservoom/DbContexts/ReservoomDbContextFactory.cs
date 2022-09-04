using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservetionDesktopApp.DbContexts
{
    public class ReservetionDesktopAppDbContextFactory
    {
        private readonly string _connectionString;

        public ReservetionDesktopAppDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservetionDesktopAppDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ReservetionDesktopAppDbContext(options);
        }
    }
}
