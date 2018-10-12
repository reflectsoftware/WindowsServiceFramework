@SET FrameworkDir=%SystemRoot%\Microsoft.NET\Framework
@SET FrameworkVersion=v4.0.30319
@set PATH=%FrameworkDir%\%FrameworkVersion%;%PATH%;

@installutil /u /servicename="WindowsServiceFramework.BasicSample.Service" "%~dp0WindowsServiceFramework.BasicSample.exe
@pause