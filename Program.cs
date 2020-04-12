using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using DatingApp.api.Data;

namespace DatingApp.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host= CreateHostBuilder(args).Build();
           using(var scope= host.Services.CreateScope())
           {
                var services=scope.ServiceProvider;
                try{
                  var context= services.GetRequiredService<DataContext>();
                  context.Database.Migrate();
                  Seed.SeedUsers(context);
                }
                catch(Exception ex){
                  var logger=services.GetRequiredService<ILogger<Program>>();
                  logger.LogError(ex.Message,"error");
                }
           }
           host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
