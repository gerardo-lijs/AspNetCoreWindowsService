using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.Linq;

namespace AspNetCoreWindowsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isConsole;
            string contentPath;

            if (Debugger.IsAttached || args.Contains("--console"))
            {
                isConsole = true;
            }
            else
            {
                isConsole = false;
            }

            //set ContenRoot directory
            if (isConsole)
                contentPath = Directory.GetCurrentDirectory();
            else
            {
                contentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                //.UseUrls("http://*:1234")  //Uncomment this line if you would like to change port. Default is 5000, but in production you probably would like to set this to a different port (from config file or commandline parameters maybe). Avoid hard-coding!
                .UseContentRoot(contentPath)
                //.UseIISIntegration()       //If you want to use IIS Express during deveplopment debug/testing you can uncomment this line but remember that RunAsService can ONLY use Kestrel. 
                .UseStartup<Startup>()
                .Build();

            //run
            if (isConsole)
                host.Run();
            else
                host.RunAsService();

            /*

            To install as a Windows Service you need to run (with Administrator privileges):
            sc create [service name] [binPath= ]   (Change ServiceName and binPath to match yours)
            Example:

            sc create AspNetCoreWindowsService binPath= "FullPathToOutputPath\AspNetCoreWindowsService.exe"

            */
        }
    }
}