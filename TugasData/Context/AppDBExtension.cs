using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugasData.Context
{
    public static class DataExtensions
    {
        public static IServiceCollection InjectAppDbRepo(this IServiceCollection services, IConfiguration cfg)
        {
            string? connectionMode = cfg.GetValue<string>("ConnectionMode");
            string? connectionString = cfg.GetValue<string>($"ConnectionStrings:{connectionMode}");

            return services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString + cfg.GetValue<string>("Database:AppDb"))
            );
        }
    }
}
