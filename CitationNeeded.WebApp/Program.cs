using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CitationNeeded.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(o => o.ListenLocalhost(5000))
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>();
    }
}
