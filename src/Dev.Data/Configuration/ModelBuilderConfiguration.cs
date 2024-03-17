

using Dev.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Context.Configurations
{
    public static class ModelBuilderConfiguration
    {
        public static void SetStringColumnType(this ModelBuilder modelBuilder, int maxLength = 100)
        {
            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string)))) 
            {
                property.SetColumnType($"varchar({maxLength})");
            }
        }
    }
}
