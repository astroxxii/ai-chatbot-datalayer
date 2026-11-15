using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TugasData.ChatCompletionServices;
using TugasData.ChatCompletionServices.Impl;

namespace TugasData.Context
{
    public static class DataExtensions
    {
        /// <summary>
        /// Registers AppDbContext with PostgreSQL using configuration keys.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="cfg">Configuration containing ConnectionMode, ConnectionStrings, and Database:AppDb.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection InjectAppDbRepo(this IServiceCollection services, IConfiguration cfg)
        {
            string? connectionMode = cfg.GetValue<string>("ConnectionMode") ?? "Default";
            string? connectionString = cfg.GetValue<string>($"ConnectionStrings:{connectionMode}");
            string? dbName = cfg.GetValue<string>("Database:AppDb");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                // Fallback to direct connection string if ConnectionStrings:{connectionMode} is not set
                connectionString = cfg.GetConnectionString(connectionMode) 
                    ?? cfg.GetConnectionString("DefaultConnection") 
                    ?? string.Empty;
            }

            string fullConnectionString = BuildConnectionString(connectionString, dbName);

            return services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(fullConnectionString)
            );
        }

        /// <summary>
        /// Helper to safely combine connection base and database name without syntax errors.
        /// </summary>
        private static string BuildConnectionString(string connectionString, string? dbName)
        {
            if (string.IsNullOrWhiteSpace(dbName))
            {
                return connectionString;
            }

            connectionString = connectionString.Trim();
            if (!connectionString.EndsWith(";") && !dbName.StartsWith(";"))
            {
                return $"{connectionString};{dbName}";
            }

            return $"{connectionString}{dbName}";
        }

        /// <summary>
        /// Registers IChatCompletionServices with HttpClient and OpenAI configuration.
        /// </summary>
        public static IServiceCollection AddChatCompletionService(this IServiceCollection services)
        {
            services.AddHttpClient<IChatCompletionServices, ChatCompletionServiceImpl>();
            return services;
        }
    }
}
