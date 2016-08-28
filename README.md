# AspNetCoreWindowsService

This is a simple sample repository to show how simple it is to run .NET Core and ASP.NET Core as a Windows Service.

I tried to keep it bare minimum in order to appreciate what it's needed. You can probably modify your current .NET Core API or ASP.NET MVC Project just by copying a few lines of this code and make ir run as a Windows Service!

Highlights:

1) You must target .NET 4.6.1 framework. Since this application is intented to run as a Windows Service it makes sense since Linux or Mac don't have a Windows service so it can't be part of the .NET Core framework.

	[project.json]

	"frameworks": {
	"net461": { }
	},

2) You have to use Kestrel when running as a Windows Service.

	[Program.cs]

	var host = new WebHostBuilder()
		.UseKestrel()
		.UseContentRoot(contentPath)
		.UseStartup<Startup>()
		.Build();

3) If you are using MVC you need to set the ContentRoot directory. This line of code is different from the templates created by Visual Studio

	[Program.cs]

    if (isConsole)
        contentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
    else
    {
        contentPath = Directory.GetCurrentDirectory();
    }

4) By default it runs as a service unless it detects you are Debugging or you pass the command line parameter --console

	[Program.cs]

    if (Debugger.IsAttached || args.Contains("--console"))
    {
        isConsole = true;
    }
    else
    {
        isConsole = false;
    }
