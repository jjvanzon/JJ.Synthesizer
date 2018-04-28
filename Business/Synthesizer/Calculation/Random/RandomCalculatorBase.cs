namespace JJ.Business.Synthesizer.Calculation.Random
{
	internal abstract class RandomCalculatorBase
	{
		protected double _offset;

		public void Reseed() => _offset = RandomCalculatorHelper.GenerateOffset();
	}
}
