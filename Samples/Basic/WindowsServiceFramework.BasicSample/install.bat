@SET FrameworkDir=%SystemRoot%\Microsoft.NET\Framework
@SET FrameworkVersion=v4.0.30319
@SET PATH=%FrameworkDir%\%FrameworkVersion%;%PATH%;

@installutil /servicename="WindowsServiceFramework.BasicSample.Service" /displayname="WindowsServiceFramework.BasicSample Service" "%~dp0WindowsServiceFramework.BasicSample.exe
@pause