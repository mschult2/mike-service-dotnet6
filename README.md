This example creates a Windows service (aka NT service) using .NET 6.0.
It uses the new model known as a "worker service", which means it's technically cross-platform.

https://docs.microsoft.com/en-us/dotnet/core/extensions/windows-service

Tools used:
- Visual Studio 2022
- .NET 6.0
- NuGet Packages: Microsoft.Extensions.Hosting.WindowsServices, Microsoft.Extensions.Http

Logs to Windows Event Viewer.
To change the ILogger default log level, modify AppSettings.<environment>.json.
Typical levels are Information, Warning, Error, None.

Publish as single-file executable.
Ready-To-Run native compilation can also be used, since it's self-contained and platform-targeted to win-x64. Remove this if you want to run on different OS's.

Install commands (admin PS):
- sc.exe create "Mike Service" binpath="C:\Repos\MikeService\bin\App\MikeService.exe"
- sc.exe start "Mike Service"

Optional configuration commands (Admin PS):
- sc.exe create "Mike Service" binpath="C:\Path\To\MikeService.exe --contentRoot C:\Other\Path"     // specify explicit content root, where AppSettings.json and other files are supposed to be
- sc.exe failure $ServiceName reset= 60 reboot= 'Mike Service has failed and will restart' actions= restart/5000/restart/10000
- sc.exe config $ServiceName start=auto

Output:
Output is logged as Application events in Event Viewer. Source is "MikeService".

Uninstall commands (Admin PS):
- sc.exe stop "Mike Service"
- sc.exe delete "Mike Service"
