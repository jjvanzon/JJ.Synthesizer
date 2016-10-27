using System;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Demos.Synthesizer.NanoOptimization
{
    [TestClass]
    public class Synthesizer_PerformanceTests_WithCSharpCompilation
    {
        [TestMethod]
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoToCSharpVisitor()
        {
            OperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoToCSharpVisitor();
            string csharp = visitor.Execute(dto);
        }
    }
}
