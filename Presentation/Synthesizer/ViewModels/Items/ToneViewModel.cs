using JetBrains.Annotations;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class ToneViewModel
	{
		public int ID { get; set; }
		public string Octave { get; set; }
		public string Value { get; set; }
		/// <summary> not editable </summary>
		public double Frequency { [UsedImplicitly] get; set; }
		/// <summary> not editable </summary>
		public int Ordinal { [UsedImplicitly] get; set; }
		/// <summary> not editable </summary>
		public int ToneNumber { [UsedImplicitly] get; set; }
	}
}