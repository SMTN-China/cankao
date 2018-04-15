using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MESCloud.EntityFrameworkCore
{
    public static class MESCloudDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MESCloudDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MESCloudDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
