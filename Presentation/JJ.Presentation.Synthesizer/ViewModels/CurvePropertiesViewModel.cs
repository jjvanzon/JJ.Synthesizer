using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurvePropertiesViewModel : ViewModelBase
    {
        public int DocumentID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary> nullable </summary>
        public IDAndName XDimension { get; set; }

        /// <summary> nullable </summary>
        public IDAndName YDimension { get; set; }

        public IList<IDAndName> DimensionLookup { get; set; }
    }
}
