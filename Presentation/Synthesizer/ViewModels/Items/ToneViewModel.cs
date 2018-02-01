namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class ToneViewModel
	{
		public int ID { get; set; }
		public string Octave { get; set; }
		public string Value { get; set; }
		/// <summary> not editable </summary>
		public double Frequency { get; set; }
		/// <summary> not editable </summary>
		public int Ordinal { get; set; }
		/// <summary> not editable </summary>
		public int ToneNumber { get; set; }
	}
}