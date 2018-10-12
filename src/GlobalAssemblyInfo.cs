using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("ReflectSoftware Inc.")]
[assembly: AssemblyCopyright("Copyright (C) 2018 ReflectSoftware Inc")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0 (rev 0)")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly : AssemblyConfiguration("Release")]
#endif

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]