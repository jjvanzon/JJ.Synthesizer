using JJ.Data.Canonical;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentDetailsViewModel : ViewModelBase
    {
        public IDAndName Document { get; set; }

        /// <summary> Do not show in the UI. Is there to persist along with the document create action. </summary>
        public AudioOutputViewModel AudioOutput { get; set; }

        public bool CanDelete { get; set; }
        public bool IDVisible { get; set; }
    }
}
