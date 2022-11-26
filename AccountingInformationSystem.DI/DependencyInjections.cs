﻿using AccountingInformationSystem.Data.EF;
using AccountingInformationSystem.Finances.Infrastructure.AutoMapper;
using AccountingInformationSystem.Finances.Interfaces;
using AccountingInformationSystem.Finances.Services;
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

        public static void AddFinances(this IServiceCollection service)
        {
            service.AddScoped<ISalaryService, SalaryService>();
        }

        public static void AddAutoMappers(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(FinancesAutoMapperProfile));
        }
    }
}