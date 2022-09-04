using Microsoft.EntityFrameworkCore;
using ReservetionDesktopApp.DTOs;
using ReservetionDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservetionDesktopApp.DbContexts
{
    public class ReservetionDesktopAppDbContext : DbContext
    {
        public ReservetionDesktopAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
