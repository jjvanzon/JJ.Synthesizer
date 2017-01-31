using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Business.Synthesizer.Visitors;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class OperatorDtoCompiler
    {
        private const string GENERATED_NAME_SPACE = "GeneratedCSharp";
        private const string GENERATED_CLASS_NAME = "GeneratedPatchCalculator";
        private const string GENERATED_CLASS_FULL_NAME = GENERATED_NAME_SPACE + "." + GENERATED_CLASS_NAME;

        private static readonly bool _includeSymbols = ConfigurationHelper.GetSection<ConfigurationSection>().IncludeSymbolsWithCompilation;
        private static readonly CSharpCompilationOptions _csharpCompilationOptions = GetCSharpCompilationOptions();

        private static readonly SyntaxTree[] _includedSyntaxTrees = CreateIncludedSyntaxTrees(
            $"Calculation\\{nameof(SineCalculator)}.cs",
            $"Calculation\\{nameof(BiQuadFilterWithoutFields)}.cs",
            $"Calculation\\Arrays\\{nameof(ArrayCalculatorBase)}.cs",
            $"Calculation\\Arrays\\{nameof(ArrayCalculatorBase_Line)}.cs",
            $"Calculation\\Arrays\\{nameof(ArrayCalculator_MinPosition_Line)}.cs",
            $"Calculation\\Arrays\\{nameof(ArrayCalculator_MinPositionZero_Line)}.cs",
            $"Calculation\\Patches\\{nameof(PatchCalculatorHelper)}.cs",
            $"CopiedCode\\FromFramework\\{nameof(MathHelper)}.cs");

        private static readonly IList<MetadataReference> _metaDataReferences = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IPatchCalculator).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(LessThanException).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Expression).Assembly.Location)
        };

        public IPatchCalculator CompileToPatchCalculator(OperatorDtoBase dto, int samplingRate, int channelCount, int channelIndex, CalculatorCache calculatorCache, ICurveRepository curveRepository)
        {
            if (dto == null) throw new NullException(() => dto);

            var preProcessingVisitor = new OperatorDtoPreProcessingExecutor(samplingRate, channelCount);
            dto = preProcessingVisitor.Execute(dto);

            var codeGenerator = new OperatorDtoToPatchCalculatorCSharpGenerator(channelCount, channelIndex, calculatorCache, curveRepository);
            OperatorDtoToPatchCalculatorCSharpGeneratorResult codeGeneratorResult = codeGenerator.Execute(dto, GENERATED_NAME_SPACE, GENERATED_CLASS_NAME);

            Dictionary<int, double[]> arrays = codeGeneratorResult.CurveCalculatorVariableInfos.ToDictionary(x => x.EntityID, x => x.Calculator._array);
            Dictionary<int, double> arrayRates = codeGeneratorResult.CurveCalculatorVariableInfos.ToDictionary(x => x.EntityID, x => x.Calculator._rate);

            Type type = Compile(codeGeneratorResult.GeneratedCode);
            var calculator = (IPatchCalculator)Activator.CreateInstance(type, samplingRate, channelCount, channelIndex, arrays, arrayRates);
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
            IList<SyntaxTree> syntaxTrees = generatedSyntaxTree.Union(_includedSyntaxTrees).ToArray();

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

                string concatinatedFailureDiagnostics = string.Join(Environment.NewLine, failureDiagnostics);
                throw new Exception("CSharpCompilation.Emit failed. " + concatinatedFailureDiagnostics);
            }

            assemblyStream.Position = 0;
            byte[] assemblyBytes = StreamHelper.StreamToBytes(assemblyStream);

            byte[] pdbBytes = null;
            if (_includeSymbols)
            {
                // ReSharper disable once PossibleNullReferenceException
                pdbStream.Position = 0;
                pdbBytes = StreamHelper.StreamToBytes(pdbStream);
            }

            Assembly assembly = Assembly.Load(assemblyBytes, pdbBytes);

            Type type = assembly.GetType(GENERATED_CLASS_FULL_NAME);

            return type;
        }

        private static CSharpCompilationOptions GetCSharpCompilationOptions()
        {
#if DEBUG
            const OptimizationLevel optimizationLevel = OptimizationLevel.Debug;
#else
            const OptimizationLevel optimizationLevel = OptimizationLevel.Release;
#endif
            // ReSharper disable once RedundantArgumentDefaultValue
            return new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: optimizationLevel);
        }

        private static SyntaxTree[] CreateIncludedSyntaxTrees(params string[] codeFilesNames)
        {
            return codeFilesNames.Select(x => CreateSyntaxTree(x)).ToArray();
        }

        private static SyntaxTree CreateSyntaxTree(string codeFileName)
        {
            string codeFileCSharp = File.ReadAllText(codeFileName);

            return CSharpSyntaxTree.ParseText(codeFileCSharp, path: codeFileName, encoding: Encoding.UTF8);
        }
    }
}