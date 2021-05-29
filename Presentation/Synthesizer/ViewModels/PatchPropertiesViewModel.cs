using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    /// <summary> Leading for saving when it comes to the simple properties. </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public sealed class PatchPropertiesViewModel : ScreenViewModelBase
    {
        public int ID { get; internal set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool Hidden { get; set; }
        public bool HasDimension { get; set; }
        public bool StandardDimensionEnabled { get; set; }
        public IDAndName StandardDimension { get; set; }
        public IList<IDAndName> StandardDimensionLookup { get; set; }
        public bool CustomDimensionNameEnabled { get; set; }
        public string CustomDimensionName { get; set; }

        internal int? OutletIDToPlay { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
