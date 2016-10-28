using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.IO;
using JJ.Framework.Reflection.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation
{
    internal class OperatorDtoToOperatorCalculatorVisitor
    {
        private const string SINE_CALCULATOR_CODE_FILE_NAME = @"Calculation\SineCalculator.cs";
        private const string GENERATED_NAMESPACE_NAME = "GeneratedCSharp";
        private const string GENERATED_CLASS_NAME = "Calculator";

        public IOperatorCalculator Execute(OperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var operatorDtoToCSharpVisitor = new OperatorDtoToCSharpVisitor();
            string calculationMethodBodyCSharp = operatorDtoToCSharpVisitor.Execute(dto);

            string calculationCodeFileCSharp = MethodBodyToCodeFileString(calculationMethodBodyCSharp);
            string sineCalculatorCodeFileCSharp = File.ReadAllText(SINE_CALCULATOR_CODE_FILE_NAME);

            var syntaxTrees = new SyntaxTree[]
            {
                CSharpSyntaxTree.ParseText(sineCalculatorCodeFileCSharp),
                CSharpSyntaxTree.ParseText(calculationCodeFileCSharp)
            };

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IOperatorCalculator).Assembly.Location)
            };

            string assemblyName = Path.GetRandomFileName();
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release));

            using (var memoryStream = new MemoryStream())
            {
                EmitResult emitResult = compilation.Emit(memoryStream);

                if (!emitResult.Success)
                {
                    IEnumerable<Diagnostic> failureDiagnostics = emitResult.Diagnostics.Where(x =>
                        x.IsWarningAsError ||
                        x.Severity == DiagnosticSeverity.Error);

                    // TODO: Use a max size of string?
                    string concatinatedFailureDiagnostics = String.Join(Environment.NewLine, failureDiagnostics.Select(x => String.Format("{0} - {1}", x.Id, x.GetMessage())));

                    throw new Exception("CSharpCompilation.Emit failed. " + concatinatedFailureDiagnostics);
                }

                memoryStream.Position = 0;
                Assembly assembly = Assembly.Load(StreamHelper.StreamToBytes(memoryStream));

                Type type = assembly.GetType(GENERATED_NAMESPACE_NAME + "." + GENERATED_CLASS_NAME);
                IOperatorCalculator calculator = (IOperatorCalculator)Activator.CreateInstance(type);

                return calculator;
            }
        }

        private string MethodBodyToCodeFileString(string methodBody)
        {
            string code = @"
using System;
using System.Runtime.CompilerServices;
using " + typeof(IOperatorCalculator).Namespace + @";
using " + typeof(SineCalculator).Namespace + @";

namespace " + GENERATED_NAMESPACE_NAME + @"
{
    public class " + GENERATED_CLASS_NAME + @" : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            " + methodBody + @"
        }
    }
}";
            return code;
        }
    }
}