using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreWindowsService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //HelloWorld example.
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"Hello World from AspNetCoreWindowsService. In real world application you would probably implement here an API or MVC controller with proper routing to interact with your Windows Service!");
            });
        }
    }
}