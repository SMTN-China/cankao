using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MESCloud.Configuration;
using MESCloud.Web;

namespace MESCloud.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MESCloudDbContextFactory : IDesignTimeDbContextFactory<MESCloudDbContext>
    {
        public MESCloudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MESCloudDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MESCloudDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MESCloudConsts.ConnectionStringName));

            return new MESCloudDbContext(builder.Options);
        }
    }
}
