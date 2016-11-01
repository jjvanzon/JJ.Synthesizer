using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

        private static readonly Encoding _encoding = Encoding.UTF8;

        private readonly bool _includeSymbols;

        public OperatorDtoToOperatorCalculatorVisitor(bool includeSymbols = true)
        {
            _includeSymbols = includeSymbols;
        }

        public IOperatorCalculator Execute(OperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var operatorDtoToCSharpVisitor = new OperatorDtoToCSharpVisitor();
            string calculationMethodBodyCSharp = operatorDtoToCSharpVisitor.Execute(dto);

            string calculationCodeFileCSharp = MethodBodyToCodeFileString(calculationMethodBodyCSharp);
            string sineCalculatorCodeFileCSharp = File.ReadAllText(SINE_CALCULATOR_CODE_FILE_NAME);

            // TODO: This code can be simplified, because many of the parameters with ParseText are optional.
            SyntaxTree calculatorSyntaxTree;
            SyntaxTree sineCalculatorSyntaxTree;
            if (_includeSymbols)
            {
                string calculationCodeFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".cs";
                File.WriteAllText(calculationCodeFileName, calculationCodeFileCSharp, _encoding);
                calculatorSyntaxTree = CSharpSyntaxTree.ParseText(calculationCodeFileCSharp, path: calculationCodeFileName, encoding: _encoding);
                sineCalculatorSyntaxTree = CSharpSyntaxTree.ParseText(sineCalculatorCodeFileCSharp, path: SINE_CALCULATOR_CODE_FILE_NAME, encoding: _encoding);
            }
            else
            {
                calculatorSyntaxTree = CSharpSyntaxTree.ParseText(calculationCodeFileCSharp);
                sineCalculatorSyntaxTree = CSharpSyntaxTree.ParseText(sineCalculatorCodeFileCSharp);
            }

            var syntaxTrees = new SyntaxTree[]
            {
                calculatorSyntaxTree,
                sineCalculatorSyntaxTree
            };

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IOperatorCalculator).Assembly.Location)
            };

#if DEBUG
            OptimizationLevel optimizationLevel = OptimizationLevel.Debug;
#else
            OptimizationLevel optimizationLevel = OptimizationLevel.Release;
#endif
            string assemblyName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: optimizationLevel));

            MemoryStream assemblyStream = new MemoryStream();
            MemoryStream pdbStream = null;
            if (_includeSymbols)
            {
                pdbStream = new MemoryStream();
            }

            EmitResult emitResult = compilation.Emit(assemblyStream, pdbStream);
            if (!emitResult.Success)
            {
                IEnumerable<Diagnostic> failureDiagnostics = emitResult.Diagnostics.Where(x =>
                    x.IsWarningAsError ||
                    x.Severity == DiagnosticSeverity.Error);

                string concatinatedFailureDiagnostics = String.Join(Environment.NewLine, failureDiagnostics.Select(x => String.Format("{0} - {1}", x.Id, x.GetMessage())));
                throw new Exception("CSharpCompilation.Emit failed. " + concatinatedFailureDiagnostics);
            }

            Assembly assembly;

            assemblyStream.Position = 0;
            byte[] assemblyBytes = StreamHelper.StreamToBytes(assemblyStream);

            if (!_includeSymbols)
            {
                assembly = Assembly.Load(assemblyBytes);
            }
            else
            {
                pdbStream.Position = 0;
                byte[] pdbBytes = StreamHelper.StreamToBytes(pdbStream);

                File.WriteAllBytes(assemblyName + ".dll", assemblyBytes);
                File.WriteAllBytes(assemblyName + ".pdb", pdbBytes);

                assembly = Assembly.Load(assemblyName);
            }

            Type type = assembly.GetType(GENERATED_NAMESPACE_NAME + "." + GENERATED_CLASS_NAME);
            IOperatorCalculator calculator = (IOperatorCalculator)Activator.CreateInstance(type);
            return calculator;
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