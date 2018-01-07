namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class MidiMappingElementItemViewModel
	{
		public int ID { get; set; }
		public string Caption { get; set; }
		public bool IsSelected { get; set; }
		public PositionViewModel Position { get; set; }
	}
}