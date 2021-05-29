using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_WithInterpolation : OperatorPropertiesViewModelBase
    {
        public IDAndName Interpolation { get; set; }
        public IList<IDAndName> InterpolationLookup { get; set; }
        public bool CanEditFollowingMode { get; set; }
        public IDAndName FollowingMode { get; set; }
        public IList<IDAndName> FollowingModeLookup { get; set; }
    }
}