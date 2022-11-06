using AccountingInformationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingInformationSystem.Data.EF
{
    public class AccountingInformationSystemContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkShedule> WorkShedules { get; set; }
        public DbSet<Shedule> Shedules { get; set; }

        public AccountingInformationSystemContext(DbContextOptions<AccountingInformationSystemContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
