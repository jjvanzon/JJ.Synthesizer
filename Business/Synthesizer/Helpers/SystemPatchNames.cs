using System;

namespace JJ.Business.Synthesizer.Helpers
{
	/// <summary> Usage: nameof(SystemPatchNames.Add) </summary>
	public static class SystemPatchNames
	{
		public static void Add() => Throw();
		public static void ClosestOverInlets() => Throw();
		public static void ClosestOverInletsExp() => Throw();
		public static void RangeOverOutlets() => Throw();
		public static void Multiply() => Throw();
		public static void MaxOverInlets() => Throw();
		public static void MinOverInlets() => Throw();
		public static void SortOverInlets() => Throw();
		public static void Sample() => Throw();

		private static void Throw() => throw new NotSupportedException("Usage: nameof(SystemPatchNames.Add)");
	}
}
