using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Calculation
{
	/// <summary>
	/// White noise is not generated on the fly, but by a cached 10 seconds of noise,
	/// which is faster and also necessary for resampling, interpolation, going back in time and other processing.
	/// 10 seconds should be enough to not notice a repeating pattern.
	/// 
	/// Each instance shares the same pre-sampled noise data, but has a different offset into it,
	/// so that there is a different virtual random set for each instance of NoiseCalculator.
	/// </summary>
	internal class NoiseCalculator : ICalculatorWithPosition
	{
		public NoiseCalculator() => Reseed();

	    private double _offset;

		/// <summary>
		/// Block interpolation should be enough,
		/// because in practice the time speed should so that each sample is a random number.
		/// </summary>
		private static readonly ArrayCalculator_RotatePosition_Block _arrayCalculator =
							new ArrayCalculator_RotatePosition_Block(NoiseCalculatorHelper.Samples, NoiseCalculatorHelper.SamplingRate);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double Calculate(double time)
		{
			double transformedTime = time + _offset;
			return _arrayCalculator.Calculate(transformedTime);
		}

		public void Reseed() => _offset = NoiseCalculatorHelper.GenerateOffset();
	}
}
