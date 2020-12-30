using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RecipeLewis.Areas.Identity.IdentityHostingStartup))]

namespace RecipeLewis.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}