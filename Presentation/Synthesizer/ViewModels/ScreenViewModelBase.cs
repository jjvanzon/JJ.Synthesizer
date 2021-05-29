using System.Collections.Generic;
using System.Diagnostics;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public abstract class ScreenViewModelBase
    {
        public bool Successful { get; set; }
        public IList<string> ValidationMessages { get; set; }
        public bool Visible { get; set; }
        public int RefreshID { get; set; }

        internal ScreenViewModelBase OriginalState { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
