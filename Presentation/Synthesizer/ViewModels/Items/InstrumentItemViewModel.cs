namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class InstrumentItemViewModel
	{
		public int EntityID { get; set; }
		public string Name { get; set; }
		public bool CanGoBackward { get; set; }
		public bool CanGoForward { get; set; }
		public bool CanPlay { get; set; }
		public bool CanDelete { get; set; }
		public bool CanExpand { get; set; }
	}
}
