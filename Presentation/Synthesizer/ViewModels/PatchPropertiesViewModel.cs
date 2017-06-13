using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    /// <summary> Leading for saving when it comes to the simple properties. </summary>
    public sealed class PatchPropertiesViewModel : ViewModelBase
    {
        public int ID { get; internal set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool Hidden { get; set; }
        public bool HasDimension { get; set; }
        public bool DefaultStandardDimensionEnabled { get; set; }
        public IDAndName DefaultStandardDimension { get; set; }
        public IList<IDAndName> DefaultStandardDimensionLookup { get; set; }
        public bool DefaultCustomDimensionNameEnabled { get; set; }
        public string DefaultCustomDimensionName { get; set; }

        internal int? OutletIDToPlay { get; set; }
    }
}
