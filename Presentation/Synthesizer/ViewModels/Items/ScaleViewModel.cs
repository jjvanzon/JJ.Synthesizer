using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class ScaleViewModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public IDAndName ScaleType { get; set; }
		public double BaseFrequency { get; set; }
	}
}
