using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace DatabaseLayer
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAcess(this IServiceCollection serviceCollection)
        {
            var databasePath = Path.Combine("DataBase", "fourthweek.db");

            serviceCollection.AddDbContext<AppDBContext>(x => x.UseSqlite($"Data Source={databasePath}"));
            return serviceCollection;
        }
    }
}
