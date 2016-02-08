using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForResample : ViewModelBase
    {
        // Properties put directly here, instead of entity view model,
        // because entity view model is too elaborate.

        public int ID { get; set; }
        public string Name { get; set; }

        public IDAndName Interpolation { get; set; }
        public IList<IDAndName> InterpolationLookup { get; set; }

        /// <summary> not editable </summary>
        public OperatorTypeViewModel OperatorType { get; set; }
    }
}