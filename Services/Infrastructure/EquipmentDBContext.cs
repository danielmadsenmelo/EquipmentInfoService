using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Infrastructure
{
    public class EquipmentDbContext : DbContext
    {
        public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options) : base(options) { }

        public DbSet<Equipment> Equipment { get; set; }
    }
}
