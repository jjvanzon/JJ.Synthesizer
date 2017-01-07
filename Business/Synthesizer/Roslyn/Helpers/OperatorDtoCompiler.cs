using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Business.Synthesizer.Roslyn.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Roslyn.Visitors;
using JJ.Framework.IO;
using JJ.Framework.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Roslyn.Generator;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class OperatorDtoCompiler
    {
        private const string SINE_CALCULATOR_CODE_FILE_NAME = @"Calculation\SineCalculator.cs";
        private const string GENERATED_NAME_SPACE = "GeneratedCSharp";
        private const string GENERATED_CLASS_NAME = "Calculator";
        private const string GENERATED_CLASS_FULL_NAME = GENERATED_NAME_SPACE + "." + GENERATED_CLASS_NAME;

        private static readonly bool _includeSymbols = ConfigurationHelper.GetSection<ConfigurationSection>().IncludeSymbolsWithCompilation;
        private static readonly IList<MetadataReference> _metaDataReferences = GetMetadataReferences();
        private static readonly CSharpCompilationOptions _csharpCompilationOptions = GetCSharpCompilationOptions();
        private static readonly SyntaxTree _sineCalculatorSyntaxTree = CreateSineCalculatorSyntaxTree();

        public IPatchCalculator CompileToPatchCalculator(OperatorDtoBase dto, int samplingRate, int targetChannelCount)
        {
            if (dto == null) throw new NullException(() => dto);

            var preProcessingVisitor = new OperatorDtoPreProcessingExecutor(targetChannelCount);
            dto = preProcessingVisitor.Execute(dto);

            var codeGeneratingVisitor = new OperatorDtoToPatchCalculatorCSharpGenerator();
            string generatedCode = codeGeneratingVisitor.Execute(dto, GENERATED_NAME_SPACE, GENERATED_CLASS_NAME);

            Type type = Compile(generatedCode);
            var calculator = (IPatchCalculator)Activator.CreateInstance(type, samplingRate);
            return calculator;
        }

        private Type Compile(string generatedCode)
        {
            string generatedCodeFileName = "";
            if (_includeSymbols)
            {
                generatedCodeFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".cs";
                File.WriteAllText(generatedCodeFileName, generatedCode, Encoding.UTF8);
            }

            SyntaxTree generatedSyntaxTree = CSharpSyntaxTree.ParseText(generatedCode, path: generatedCodeFileName, encoding: Encoding.UTF8);

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

                string concatinatedFailureDiagnostics = String.Join(Environment.NewLine, failureDiagnostics);
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

            return type;
        }

        private static IList<MetadataReference> GetMetadataReferences()
        {
            return new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IPatchCalculator).Assembly.Location)
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