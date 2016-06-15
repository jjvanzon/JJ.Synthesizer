using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_WithInletCount : OperatorPropertiesViewModelBase
    {
        public int PatchID { get; internal set; }
        public string Name { get; set; }
        public IDAndName OperatorType { get; set; }
        public int InletCount { get; set; }
    }
}
