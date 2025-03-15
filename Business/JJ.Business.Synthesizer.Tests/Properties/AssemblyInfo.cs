using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("13f29762-103c-4ff9-b178-e6c5202d1254")]

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)] 
[assembly: SuppressMessage("MSTest", "MSTEST0018", Justification = "CaseCollection can be used as DynamicData.")]
[assembly: SuppressMessage("IDE", "IDE0002", Justification = "Static and using static syntax both considered equally valid.")]
[assembly: SuppressMessage("IDE", "IDE0051", Justification = "Remove unused private members - Fades out members marked [UsedImplicity].")]
