namespace JJ.Business.Synthesizer.Calculation.Random
{
	internal abstract class RandomCalculatorBase
	{
		protected double _offset;

		public abstract ICalculatorWithPosition UnderlyingArrayCalculator { get; }

		public void Reseed()
		{
			_offset = RandomCalculatorHelper.GenerateOffset();
		}
	}
}
