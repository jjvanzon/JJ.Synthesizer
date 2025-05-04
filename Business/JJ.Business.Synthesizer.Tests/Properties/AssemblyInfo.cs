using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)] 
[assembly: SuppressMessage("MSTest", "MSTEST0018", Justification = "CaseCollection can be used as DynamicData.")]
[assembly: SuppressMessage("IDE", "IDE0002", Justification = "Static and using static syntax both considered equally valid.")]
[assembly: SuppressMessage("IDE", "IDE0051", Justification = "Remove unused private members - Fades out members marked [UsedImplicity].")]
