using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        private const string GENERATED_NAME_SPACE = "GeneratedCSharp";
        private const string GENERATED_CLASS_NAME = "Calculator";
        private const string GENERATED_CLASS_FULL_NAME = GENERATED_NAME_SPACE + "." + GENERATED_CLASS_NAME;

        private static readonly IList<MetadataReference> _metaDataReferences = GetMetadataReferences();
        private static readonly CSharpCompilationOptions _csharpCompilationOptions = GetCSharpCompilationOptions();
        private static readonly SyntaxTree _sineCalculatorSyntaxTree = CreateSineCalculatorSyntaxTree();

        private readonly bool _includeSymbols;
        private readonly Encoding _encoding;

        public OperatorDtoToOperatorCalculatorVisitor(bool includeSymbols = true)
        {
            _includeSymbols = includeSymbols;
            if (_includeSymbols)
            {
                _encoding = Encoding.UTF8;
            };
        }

        public IOperatorCalculator Execute(OperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var operatorDtoToCSharpVisitor = new OperatorDtoToCSharpVisitor();
            string generatedCode = operatorDtoToCSharpVisitor.Execute(dto, GENERATED_NAME_SPACE, GENERATED_CLASS_NAME);

            string generatedCodeFileName = "";
            if (_includeSymbols)
            {
                generatedCodeFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".cs";
                File.WriteAllText(generatedCodeFileName, generatedCode, _encoding);
            }

            SyntaxTree generatedSyntaxTree = CSharpSyntaxTree.ParseText(generatedCode, path: generatedCodeFileName, encoding: _encoding);

            var syntaxTrees = new SyntaxTree[]
            {
                generatedSyntaxTree,
                _sineCalculatorSyntaxTree
            };

            string assemblyName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees,
                _metaDataReferences,
                _csharpCompilationOptions);

            var assemblyStream = new MemoryStream();
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

            assemblyStream.Position = 0;
            byte[] assemblyBytes = StreamHelper.StreamToBytes(assemblyStream);

            byte[] pdbBytes = null;
            if (_includeSymbols)
            {
                pdbStream.Position = 0;
                pdbBytes = StreamHelper.StreamToBytes(pdbStream);
            }

            Assembly assembly = Assembly.Load(assemblyBytes, pdbBytes);

            Type type = assembly.GetType(GENERATED_CLASS_FULL_NAME);
            IOperatorCalculator calculator = (IOperatorCalculator)Activator.CreateInstance(type);
            return calculator;
        }

        private static IList<MetadataReference> GetMetadataReferences()
        {
            return new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IOperatorCalculator).Assembly.Location)
            };
        }

        private static CSharpCompilationOptions GetCSharpCompilationOptions()
        {
#if DEBUG
            OptimizationLevel optimizationLevel = OptimizationLevel.Debug;
#else
            OptimizationLevel optimizationLevel = OptimizationLevel.Release;
#endif
            return new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: optimizationLevel);
        }

        private static SyntaxTree CreateSineCalculatorSyntaxTree()
        {
            string sineCalculatorCodeFileCSharp = File.ReadAllText(SINE_CALCULATOR_CODE_FILE_NAME);

            return CSharpSyntaxTree.ParseText(sineCalculatorCodeFileCSharp, path: SINE_CALCULATOR_CODE_FILE_NAME, encoding: Encoding.UTF8);
        }
    }
}