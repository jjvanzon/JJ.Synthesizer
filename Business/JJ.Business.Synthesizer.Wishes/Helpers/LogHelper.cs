using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class LogHelper
    {
        public static void LogComputeConstant(
            FluentOutlet a, string mathSymbol, FluentOutlet b, FluentOutlet result,
            [CallerMemberName] string opName = null)
            => Console.WriteLine($"Compute const : {Stringify(opName, a, mathSymbol, b)} => {Stringify(result)}");
        
        public static void LogIdentityOperation(
            FluentOutlet a, string mathSymbol, FluentOutlet identityValue,
            [CallerMemberName] string opName = null)
            => Console.WriteLine($"Identity op : {Stringify(opName, a, mathSymbol, identityValue)} => {Stringify(a)}");
        
        public static void LogIdentityOperation(
            FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine($"Identity op ({dimension}) : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        public static void LogAlwaysOneOptimization(
            FluentOutlet a, string mathSymbol, FluentOutlet b,
            [CallerMemberName] string opName = null)
            => Console.WriteLine($"Always 1 : {Stringify(opName, a, mathSymbol, b)} => 1");
        
        public static void LogAlwaysOneOptimization(
            FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine($"Always 1 ({dimension}) : " +
                                 $"{Stringify(opName, signal, dimension, mathSymbol, transform)} => " +
                                 $"{Stringify(opName, signal, dimension, "=", 1)}");
        
        public static void LogInvariance(
            FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine($"Invariance ({dimension}) : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        public static void LogDivisionByMultiplication(FluentOutlet a, FluentOutlet b, FluentOutlet result)
            => Console.WriteLine($"/ by * : {Stringify(a)} / {Stringify(b)} => {Stringify(result)}");
        
        public static void LogDistributeMultiplyOverAddition(FluentOutlet formulaBefore, FluentOutlet formulaAfter)
            => Console.WriteLine($"Distribute * over + : {Stringify(formulaBefore)} => {Stringify(formulaAfter)}");
        
        public static void LogAdditionOptimizations(
            IList<FluentOutlet> terms, IList<FluentOutlet> flattenedTerms, IList<FluentOutlet> optimizedTerms,
            IList<FluentOutlet> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "+";
            
            bool wasFlattened = terms.Count != flattenedTerms.Count;
            if (wasFlattened)
            {
                Console.WriteLine($"Flatten {symbol} : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, flattenedTerms)}");
            }
            
            bool hasConst0 = consts.Count >= 1 && constant == 0;
            if (hasConst0)
            {
                Console.WriteLine($"Eliminate 0 : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine($"Compute const : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool noTermsLeft = terms.Count != 0 && optimizedTerms.Count == 0;
            if (noTermsLeft)
            {
                Console.WriteLine($"0 terms remain : {Stringify(opName, symbol, terms)} => 0");
            }
            
            bool oneTermLeft = optimizedTerms.Count == 1;
            if (oneTermLeft)
            {
                Console.WriteLine($"Eliminate {symbol} : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(symbol, optimizedTerms)}");
            }
        }
        
        public static void LogMultiplicationOptimizations(
            IList<FluentOutlet> factors, IList<FluentOutlet> optimizedFactors,
            IList<FluentOutlet> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "*";
            
            bool hasConst1 = consts.Count >= 1 && constant == 1;
            if (hasConst1)
            {
                Console.WriteLine($"Eliminate 1 : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine($"Compute const : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool noFactorsLeft = factors.Count != 0 && optimizedFactors.Count == 0;
            if (noFactorsLeft)
            {
                Console.WriteLine($"0 factors remain: {Stringify(opName, symbol, factors)} => 1");
            }
            
            bool oneFactorLeft = optimizedFactors.Count == 1;
            if (oneFactorLeft)
            {
                Console.WriteLine($"Eliminate {symbol} : {Stringify(opName, symbol, optimizedFactors)} => {Stringify(symbol, optimizedFactors)}");
            }
        }
        
        // Specialized Stringifications
        
        internal static string Stringify(string opName, FluentOutlet a, string mathSymbol, FluentOutlet b)
            => Stringify(opName, mathSymbol, a, b);
        
        internal static string Stringify(string opName, string mathSymbol, params FluentOutlet[] operands)
            => Stringify(opName, mathSymbol, (IList<FluentOutlet>)operands);
        
        internal static string Stringify(string opName, string mathSymbol, IList<FluentOutlet> operands)
            => $"{opName}({Stringify(mathSymbol, operands)})";
        
        internal static string Stringify(string mathSymbol, IList<FluentOutlet> operands)
            => string.Join(" " + mathSymbol + " ", operands.Select(Stringify));
        
        internal static string Stringify(FluentOutlet operand)
            => operand.Stringify(true);
        
        internal static string Stringify(
            string opName, FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {Stringify(transform)})";
        
        internal static string Stringify(
            string opName, FluentOutlet signal, string dimension, string mathSymbol, double value)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {value})";
    }
}