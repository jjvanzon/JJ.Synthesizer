using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    /// <summary> Usage: nameof(SystemPatchNames.Add) </summary>
    public static class SystemPatchNames
    {
        public static void Add() => Throw();
        public static void AverageFollower() => Throw();
        public static void AverageOverDimension() => Throw();
        public static void AverageOverInlets() => Throw();
        public static void Cache() => Throw();
        public static void ClosestOverDimension() => Throw();
        public static void ClosestOverDimensionExp() => Throw();
        public static void ClosestOverInlets() => Throw();
        public static void ClosestOverInletsExp() => Throw();
        public static void Curve() => Throw();
        public static void DimensionToOutlets() => Throw();
        public static void InletsToDimension() => Throw();
        public static void Interpolate() => Throw();
        public static void MaxFollower() => Throw();
        public static void MaxOverDimension() => Throw();
        public static void MaxOverInlets() => Throw();
        public static void MinFollower() => Throw();
        public static void MinOverDimension() => Throw();
        public static void MinOverInlets() => Throw();
        public static void Multiply() => Throw();
        public static void MultiplyWithOrigin() => Throw();
        public static void Number() => Throw();
        public static void PatchInlet() => Throw();
        public static void PatchOutlet() => Throw();
        public static void Random() => Throw();
        public static void RangeOverOutlets() => Throw();
        public static void Reset() => Throw();
        public static void Sample() => Throw();
        public static void SortOverDimension() => Throw();
        public static void SortOverInlets() => Throw();
        public static void Subtract() => Throw();
        public static void SumFollower() => Throw();
        public static void SumOverDimension() => Throw();

        private static void Throw() => throw new NameOfOnlyException();
    }
}