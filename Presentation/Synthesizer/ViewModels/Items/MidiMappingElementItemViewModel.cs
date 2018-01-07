namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class MidiMappingElementItemViewModel
	{
		public int ID { get; set; }
		public int EntityPositionID { get; set; }
		public string Caption { get; set; }
		public float CenterX { get; set; }
		public float CenterY { get; set; }
		public bool IsSelected { get; set; }
	}
}