using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PigsInTheWoods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Data
{
    public interface ISeedData
    {
        Task Seed(ApplicationDbContext context, IServiceProvider serviceProvider);
    }

    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, ISeedData seed)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.Migrate();

                seed.Seed(context, serviceProvider).Wait();
            }
        }
    }
}
