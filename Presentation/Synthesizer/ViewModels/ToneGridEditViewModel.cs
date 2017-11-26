using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class ToneGridEditViewModel : ViewModelBase
	{
		public int ScaleID { get; set; }
		public IList<ToneViewModel> Tones { get; set; }

		/// <summary> The title of the number column might vary with the ScaleType. </summary>
		public string NumberTitle { get; set; }
		public bool FrequencyVisible { get; set; }
		internal int? OutletIDToPlay { get; set; }
		public int CreatedToneID { get; set; }
	}
}
