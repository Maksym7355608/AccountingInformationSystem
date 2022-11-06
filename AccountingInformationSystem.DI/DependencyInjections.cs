using AccountingInformationSystem.BL.Shedule.Interfaces;
using AccountingInformationSystem.BL.Shedule.Services;
using AccountingInformationSystem.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingInformationSystem.DI
{
    public static class DependencyInjections
    {
        public static void AddDatabase(this IServiceCollection service, string connectionString)
        {
            if(connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            service.AddDbContext<AccountingInformationSystemContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddFinaces(this IServiceCollection service)
        {
            service.AddScoped<ISalaryService, SalaryService>();
        }
    }
}