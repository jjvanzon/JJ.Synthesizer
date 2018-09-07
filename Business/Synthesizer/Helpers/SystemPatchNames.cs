using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    /// <summary> Usage: nameof(SystemPatchNames.Add) </summary>
    public static class SystemPatchNames
    {
        public static void Add() => Throw();
        public static void Absolute() => Throw();
        public static void And() => Throw();
        public static void ArcCos() => Throw();
        public static void ArcSin() => Throw();
        public static void ArcTan() => Throw();
        public static void AverageFollower() => Throw();
        public static void AverageOverDimension() => Throw();
        public static void AverageOverInlets() => Throw();
        public static void Cache() => Throw();
        public static void Ceiling() => Throw();
        public static void ClosestOverDimension() => Throw();
        public static void ClosestOverDimensionExp() => Throw();
        public static void ClosestOverInlets() => Throw();
        public static void ClosestOverInletsExp() => Throw();
        public static void Cos() => Throw();
        public static void CosH() => Throw();
        public static void CubeRoot() => Throw();
        public static void Curve() => Throw();
        public static void DimensionToOutlets() => Throw();
        public static void Divide() => Throw();
        public static void DivideWithOrigin() => Throw();
        public static void E() => Throw();
        public static void Equal() => Throw();
        public static void Floor() => Throw();
        public static void GreaterThan() => Throw();
        public static void GreaterThanOrEqual() => Throw();
        public static void InletsToDimension() => Throw();
        public static void Interpolate() => Throw();
        public static void LessThan() => Throw();
        public static void LessThanOrEqual() => Throw();
        public static void LogN() => Throw();
        public static void Ln() => Throw();
        public static void MaxFollower() => Throw();
        public static void MaxOverDimension() => Throw();
        public static void MaxOverInlets() => Throw();
        public static void MinFollower() => Throw();
        public static void MinOverDimension() => Throw();
        public static void MinOverInlets() => Throw();
        public static void Multiply() => Throw();
        public static void MultiplyWithOrigin() => Throw();
        public static void Nand() => Throw();
        public static void Negative() => Throw();
        public static void Not() => Throw();
        public static void NotEqual() => Throw();
        public static void NthRoot() => Throw();
        public static void Number() => Throw();
        public static void OneOverX() => Throw();
        public static void Or() => Throw();
        public static void PatchInlet() => Throw();
        public static void PatchOutlet() => Throw();
        public static void Pi() => Throw();
        public static void Power() => Throw();
        public static void Remainder() => Throw();
        public static void Random() => Throw();
        public static void RangeOverOutlets() => Throw();
        public static void Reset() => Throw();
        public static void Sample() => Throw();
        public static void Sign() => Throw();
        public static void Sin() => Throw();
        public static void SinH() => Throw();
        public static void SortOverDimension() => Throw();
        public static void SortOverInlets() => Throw();
        public static void SquareRoot() => Throw();
        public static void Subtract() => Throw();
        public static void SumFollower() => Throw();
        public static void SumOverDimension() => Throw();
        public static void Tan() => Throw();
        public static void TanH() => Throw();
        public static void Truncate() => Throw();
        public static void TwoPi() => Throw();
        public static void Xor() => Throw();

        private static void Throw() => throw new NameOfOnlyException();
    }
}