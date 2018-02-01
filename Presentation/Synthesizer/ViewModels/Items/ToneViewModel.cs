namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class ToneViewModel
	{
		public int ID { get; set; }
		public int ToneNumber { get; set; }
		public string Octave { get; set; }
		public string Value { get; set; }
		/// <summary> not editable </summary>
		public double Frequency { get; set; }
	}
}