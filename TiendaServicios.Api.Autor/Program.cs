using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Autor
{
    /*  ====== IntelliSense summary solution c# ======
        1. Go to the solution explorer of your source code. Right click on the project name and go the properties.
        2. go to the Build tab if you are using c# and select the check box Xml documentation file.
        3. When you build your source code the Xml file will be generated in the location where your dll is present.
        4. while copy your dll to the solution copy the xml file and paste into the bin of your destination solution.
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
