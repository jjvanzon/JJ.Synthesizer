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
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.IO;
using JJ.Framework.Mathematics;
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

        private static readonly bool _includeSymbols = CustomConfigurationManager.GetSection<ConfigurationSection>().IncludeSymbolsWithCompilation;
        private static readonly CSharpCompilationOptions _csharpCompilationOptions = GetCSharpCompilationOptions();

        private static readonly SyntaxTree[] _includedSyntaxTrees =
            CreateIncludedSyntaxTreesFromEmbeddedResources(
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(SineCalculator)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPosition_Block)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPosition_Cubic)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPosition_Hermite)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPosition_Line)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPosition_Stripe)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPositionZero_Block)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPositionZero_Cubic)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPositionZero_Hermite)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPositionZero_Line)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPositionZero_Stripe)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_MinPositionZero_Stripe_NoRate)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Block)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Block_NoRate)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Cubic)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Hermite)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Line)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Line_NoRate)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Stripe)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculator_RotatePosition_Stripe_NoRate)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorBase)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorBase_Block)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorBase_Cubic)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorBase_Hermite)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorBase_Line)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorBase_Stripe)}.cs"),
                    (Assembly.GetExecutingAssembly(), "EmbeddedResources", $"{nameof(ArrayCalculatorFactory)}.cs")
                )
                .Union(
                    CreateIncludedSyntaxTreesFromFiles(
                        $"Calculation\\{nameof(BiQuadFilterWithoutFields)}.cs",
                        $"Calculation\\Patches\\{nameof(PatchCalculatorHelper)}.cs",
                        $"CopiedCode\\FromFramework\\{nameof(Geometry)}.cs",
                        $"CopiedCode\\FromFramework\\{nameof(Interpolator)}.cs",
                        $"CopiedCode\\FromFramework\\{nameof(MathHelper)}.cs",
                        $"Helpers\\{nameof(CalculationHelper)}.cs")
                )
                .ToArray();

        private static readonly IList<MetadataReference> _metaDataReferences = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IPatchCalculator).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(LessThanException).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Expression).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ConfigurationHelper).Assembly.Location),
            // NOTE: Referencing JJ.Framework.Mathematics is a little 'dangerous', since then you could more easily fall into the trap
            // of not using the classes copied from JJ.Framework.Mathematics to JJ.Business.Synthesizer, copied to promote inlining.
            // Inlining is a big deal in this calculation engine.
            MetadataReference.CreateFromFile(typeof(Randomizer).Assembly.Location)
        };

        [Obsolete("Consider using CompileToPatchCalculatorActivationInfo instead, to compile once and instantiate multiple times.")]
        public IPatchCalculator CompileToPatchCalculator(IOperatorDto dto, int samplingRate, int channelCount, int channelIndex)
        {
            ActivationInfo activationInfo = CompileToPatchCalculatorActivationInfo(dto, samplingRate, channelCount, channelIndex);

            var calculator = (IPatchCalculator)Activator.CreateInstance(activationInfo.Type, activationInfo.Args);

            return calculator;
        }

        public ActivationInfo CompileToPatchCalculatorActivationInfo(
            IOperatorDto dto,
            int samplingRate,
            int channelCount,
            int channelIndex)
        {
            if (dto == null) throw new NullException(() => dto);

            var codeGenerator = new OperatorDtoToPatchCalculatorCSharpGenerator(channelCount, channelIndex);

            OperatorDtoToPatchCalculatorCSharpGeneratorResult codeGeneratorResult = codeGenerator.Execute(
                dto,
                GENERATED_NAME_SPACE,
                GENERATED_CLASS_NAME);

            Type type = Compile(codeGeneratorResult.GeneratedCode);

            Dictionary<string, ArrayDto> arrayDtoDictionary =
                codeGeneratorResult.ArrayCalculationInfos.ToDictionary(x => x.NameCamelCase, x => x.ArrayDto);

            var args = new object[]
            {
                samplingRate,
                channelCount,
                channelIndex,
                arrayDtoDictionary
            };

            return new ActivationInfo(type, args);
        }

        private Type Compile(string generatedCode)
        {
            var generatedCodeFileName = "";

            if (_includeSymbols)
            {
                generatedCodeFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".cs";
                File.WriteAllText(generatedCodeFileName, generatedCode, Encoding.UTF8);
            }

            SyntaxTree generatedSyntaxTree = CSharpSyntaxTree.ParseText(generatedCode, path: generatedCodeFileName, encoding: Encoding.UTF8);
            IList<SyntaxTree> syntaxTrees = generatedSyntaxTree.Concat(_includedSyntaxTrees).ToArray();

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
                IEnumerable<Diagnostic> failureDiagnostics = emitResult.Diagnostics.Where(
                    x =>
                        x.IsWarningAsError ||
                        x.Severity == DiagnosticSeverity.Error);

                string concatenatedFailureDiagnostics = string.Join(Environment.NewLine, failureDiagnostics);
                throw new Exception("CSharpCompilation.Emit failed. " + concatenatedFailureDiagnostics);
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

        private static SyntaxTree[] CreateIncludedSyntaxTreesFromFiles(params string[] codeFilesNames)
            => codeFilesNames.Select(CreateSyntaxTreeFromFile).ToArray();

        private static SyntaxTree CreateSyntaxTreeFromFile(string codeFileName)
        {
            string cSharp = File.ReadAllText(codeFileName);
            return CSharpSyntaxTree.ParseText(cSharp, path: codeFileName, encoding: Encoding.UTF8);
        }

        private static SyntaxTree[] CreateIncludedSyntaxTreesFromEmbeddedResources(
            params (Assembly assembly, string subNameSpace, string fileName)[] tuples)
            => tuples.Select(x => CreateSyntaxTreeFromEmbeddedResource(x.assembly, x.subNameSpace, x.fileName)).ToArray();

        private static SyntaxTree CreateSyntaxTreeFromEmbeddedResource(Assembly assembly, string subNameSpace, string fileName)
        {
            string cSharp = EmbeddedResourceHelper.GetEmbeddedResourceText(assembly, subNameSpace, fileName);
            return CSharpSyntaxTree.ParseText(cSharp, path: fileName, encoding: Encoding.UTF8);
        }
    }
}