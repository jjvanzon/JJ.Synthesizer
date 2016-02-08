using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForUnbundle : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int OutletCount { get; set; }
    }
}
