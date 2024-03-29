﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Roslyn.Calculation;
using JJ.Business.SynthesizerPrototype.Roslyn.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.Roslyn.Generators;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Helpers
{
    public class OperatorDtoCompiler
    {
        private const string SINE_CALCULATOR_CODE_FILE_NAME = @"CopiedCode\From_JJ_Business_SynthesizerPrototype\SineCalculator.cs";
        private const string GENERATED_NAME_SPACE = "GeneratedCSharp";
        private const string GENERATED_CLASS_NAME = "Calculator";
        private const string GENERATED_CLASS_FULL_NAME = GENERATED_NAME_SPACE + "." + GENERATED_CLASS_NAME;

        private static readonly IList<MetadataReference> _metaDataReferences = GetMetadataReferences();
        private static readonly CSharpCompilationOptions _csharpCompilationOptions = GetCSharpCompilationOptions();
        private static readonly SyntaxTree _sineCalculatorSyntaxTree = CreateSineCalculatorSyntaxTree();

        private readonly bool _includeSymbols;
        private readonly Encoding _encoding;

        public OperatorDtoCompiler(bool includeSymbols = true)
        {
            _includeSymbols = includeSymbols;
            if (_includeSymbols)
            {
                _encoding = Encoding.UTF8;
            }
        }

        public IOperatorCalculator CompileToOperatorCalculator(IOperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var preProcessingVisitor = new OperatorDtoPreProcessingExecutor();
            dto = preProcessingVisitor.Execute(dto);

            var codeGeneratingVisitor = new OperatorDtoToOperatorCalculatorCSharpGenerator();
            string generatedCode = codeGeneratingVisitor.Execute(dto, GENERATED_NAME_SPACE, GENERATED_CLASS_NAME);

            Type type = Compile(generatedCode);
            var calculator = (IOperatorCalculator)Activator.CreateInstance(type);
            return calculator;
        }

        public IPatchCalculator CompileToPatchCalculator(IOperatorDto dto, int framesPerChunk)
        {
            if (dto == null) throw new NullException(() => dto);

            var preProcessingVisitor = new OperatorDtoPreProcessingExecutor();
            dto = preProcessingVisitor.Execute(dto);

            var codeGeneratingVisitor = new OperatorDtoToPatchCalculatorCSharpGenerator();
            string generatedCode = codeGeneratingVisitor.Execute(dto, GENERATED_NAME_SPACE, GENERATED_CLASS_NAME);

            Type type = Compile(generatedCode);
            var calculator = (IPatchCalculator)Activator.CreateInstance(type, framesPerChunk);
            return calculator;
        }

        private Type Compile(string generatedCode)
        {
            string generatedCodeFileName = "";
            if (_includeSymbols)
            {
                generatedCodeFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".cs";
                File.WriteAllText(generatedCodeFileName, generatedCode, _encoding);
            }

            SyntaxTree generatedSyntaxTree = CSharpSyntaxTree.ParseText(generatedCode, path: generatedCodeFileName, encoding: _encoding);

            SyntaxTree[] syntaxTrees =
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
                IEnumerable<Diagnostic> failureDiagnostics = emitResult.Diagnostics.Where(
                    x =>
                        x.IsWarningAsError ||
                        x.Severity == DiagnosticSeverity.Error);

                string concatenatedFailureDiagnostics = string.Join(Environment.NewLine, failureDiagnostics.Select(x => $"{x.Id} - {x.GetMessage()}"));
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

        private static IList<MetadataReference> GetMetadataReferences()
            => new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IOperatorCalculator).Assembly.Location)
            };

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

        private static SyntaxTree CreateSineCalculatorSyntaxTree()
        {
            string sineCalculatorCodeFileCSharp = EmbeddedResourceHelper.GetEmbeddedResourceText(
                typeof(SineCalculator).Assembly,
                "CopiedCode.From_JJ_Business_SynthesizerPrototype",
                "SineCalculator_EmbeddedResource.cs");

            return CSharpSyntaxTree.ParseText(sineCalculatorCodeFileCSharp, path: SINE_CALCULATOR_CODE_FILE_NAME, encoding: Encoding.UTF8);
        }
    }
}