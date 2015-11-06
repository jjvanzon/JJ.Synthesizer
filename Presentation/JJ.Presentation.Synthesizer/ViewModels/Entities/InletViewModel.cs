using System.Diagnostics;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public sealed class InletViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary> Does not necessarily need to be displayed, but does need to be passed around, so it is present when a new object is saved. </summary>
        public int ListIndex { get; set; }

        /// <summary> nullable </summary>
        public OutletViewModel InputOutlet { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
